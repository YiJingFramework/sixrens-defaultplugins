using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.工具;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.神煞
{
    internal static class 神煞测试实现
    {
        private sealed record 壬式(I年月日时 年月日时);

        private delegate EarthlyBranch? 取神煞法(壬式 式);

        private static class 取神煞方法
        {
            #region 各神煞
            public static 取神煞法 太岁 => (式) => 式.年月日时.年支;
            public static 取神煞法 岁破 => (式) => 式.年月日时.年支.取冲();
            public static 取神煞法 月破 => (式) => 式.年月日时.月支.取冲();
            public static 取神煞法 日德 => (式) =>
            {
                var 日干 = 式.年月日时.日干;
                return 日干.阴阳().IsYang ? 日干.寄宫() : 日干.取所合干().寄宫();
            };
            public static 取神煞法 日禄 => (式) =>
            {
                return new EarthlyBranch(
                    式.年月日时.日干.Index switch
                    {
                        1 => 3, // 甲禄在寅
                        2 => 4, // 乙禄在卯
                        3 => 6, // 丙禄在巳
                        4 => 7, // 丁禄在午
                        5 => 6, // 戊禄在巳
                        6 => 7, // 己禄在午
                        7 => 9, // 庚禄在申
                        8 => 10, // 辛禄在酉
                        9 => 12, // 壬禄在亥
                        _ => 1 // 癸禄在子
                    });
                // var 日干 = 式.年月日时.日干;
                // return 日干.阴阳().IsYang ? 日干.五行().五行以长生取支(十二长生.临官) : 日干.取所合干().寄宫();
            };
            public static 取神煞法 羊刃 => (式) =>
            {
                return 式.年月日时.日干.Index switch
                {
                    1 => new EarthlyBranch(4), // 甲刃在卯
                    3 or 5 => new EarthlyBranch(7), // 丙戊在午
                    7 => new EarthlyBranch(10), // 庚刃在酉
                    9 => new EarthlyBranch(1), // 壬刃在子
                    _ => null // 阴干无刃
                };
                // var 日干 = 式.年月日时.日干;
                // return 日干.阴阳().IsYang ? 日干.五行().五行以长生取支(十二长生.帝旺) : null;
            };
            public static 取神煞法 闭口 => (式) =>
            {
                var 癸 = new HeavenlyStem(10);
                return 式.年月日时.旬所在.获取对应地支(癸);
            };
            public static 取神煞法 驿马 => (式) =>
            {
                var 日支 = 式.年月日时.日支;
                return 日支.取所在三合局().合化五行.以长生取支(十二长生.病);
            };
            public static 取神煞法 丁马 => (式) =>
            {
                var 丁 = new HeavenlyStem(4);
                return 式.年月日时.旬所在.获取对应地支(丁);
            };
            public static 取神煞法 天马 => (式) =>
            {
                const int 午数 = 7;
                const int 寅数 = 3;
                return new EarthlyBranch(午数 + (式.年月日时.月支.Index - 寅数) * 2);
            };
            public static 取神煞法 生气 => (式) =>
            {
                const int 子数 = 1;
                const int 寅数 = 3;
                return new EarthlyBranch(子数 + (式.年月日时.月支.Index - 寅数));
            };
            public static 取神煞法 死气 => (式) => 生气(式)?.取冲();
            #endregion 各神煞
        }

        private static readonly IReadOnlyDictionary<string, 取神煞法> 取神煞法列表;

        static 神煞测试实现()
        {
            取神煞法列表 = typeof(取神煞方法).GetProperties().ToDictionary(
                (p) => p.Name,
                (p) =>
                {
                    var r = p.GetValue(null);
                    Debug.Assert(r is 取神煞法);
                    return (取神煞法)r;
                });
        }

        private sealed record 神煞题目(
            string 神煞名) : I神煞题目
        { }
        private sealed record 神煞内容(
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞内容
        { }
        public static IEnumerable<I神煞题目> 支持的神煞 => 
            取神煞法列表.Keys.Select(神煞名 => new 神煞题目(神煞名));

        public static I神煞内容 取煞(I年月日时 年月日时, I神煞题目 题目)
        {
            壬式 式 = new(年月日时);
            if (!取神煞法列表.TryGetValue(题目.神煞名, out var 取法))
                throw new 起课失败异常($"不支持的神煞题目：{题目.神煞名}");

            var 结果 = 取法(式);
            if (结果.HasValue)
                return new 神煞内容(Array.AsReadOnly(new[] { 结果.Value }));
            else
                return new 神煞内容(Array.Empty<EarthlyBranch>());
        }
    }
}
