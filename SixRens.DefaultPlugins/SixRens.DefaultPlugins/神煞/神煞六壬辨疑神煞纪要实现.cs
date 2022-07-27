using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.工具;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.神煞
{
    internal static class 神煞六壬辨疑神煞纪要实现
    {
        private sealed record 壬式(I年月日时 年月日时);

        private delegate EarthlyBranch? 取神煞法(壬式 式);

        private delegate EarthlyBranch[] 取多神煞法(壬式 式);

        private static class 取神煞方法
        {
            private static EarthlyBranch 某日月起某某行十二支(
                int 某日月, int 起某, EarthlyBranch 所至, int 步长 = 1)
            {
                int 所行 = 所至.Index - 某日月;
                return new(起某 + 所行 * 步长);
            }
            private static EarthlyBranch 马(EarthlyBranch 支)
            {
                return 支.取所在三合局().长生支.取冲();
            }

            #region 各神煞

            #region 干煞
            public static 取神煞法 日德 => (式) => {
                // 阳干同禄神，阴干从官星之禄神。如乙以庚为官用申之类。
                var 日干 = 式.年月日时.日干;
                if (!日干.阴阳().IsYang)
                    日干 = 日干.取所合干();
                return 日干.以长生取支(十二长生.临官);
            };
            public static 取神煞法 日合 => (式) => {
                // 日干对宫之神。如甲用己之类。
                var 日干 = 式.年月日时.日干;
                return 日干.取所合干().寄宫();
            };
            public static 取神煞法 游都 => (式) => {
                // 甲己在丑，乙庚在子，丙辛在寅，丁壬在巳，戊癸在申。
                return new EarthlyBranch(
                    式.年月日时.日干.Index switch {
                        1 or 6 => 2,
                        2 or 7 => 1,
                        3 or 8 => 3,
                        4 or 9 => 6,
                        _ => 9
                    });
            };
            public static 取神煞法 鲁都 => (式) => {
                // 即游都对宫之神。
                var 游 = 游都(式);
                Debug.Assert(游.HasValue);
                return 游.Value.取冲();
            };
            public static 取神煞法 干奇 => (式) => {
                // 午巳辰卯寅丑未申酉戌
                return new EarthlyBranch(
                    式.年月日时.日干.Index switch {
                        1 => 7,
                        2 => 6,
                        3 => 5,
                        4 => 4,
                        5 => 3,
                        6 => 2,
                        7 => 8,
                        8 => 9,
                        9 => 10,
                        _ => 11
                    });
            };
            #endregion

            #region 支煞
            public static 取神煞法 支德 => (式) => {
                // 子日起巳，顺行十二支。
                var 日支 = 式.年月日时.日支;
                return 某日月起某某行十二支(1, 6, 日支);
            };
            /*
            public static 取神煞法 六合 => (式) =>
            {
                // 子丑合，寅亥合，卯戌合，辰酉合，巳申合，午未合。
                var 日支 = 式.年月日时.日支;
                return 日支.取六合();
            };
            public static 取多神煞法 三合 => (式) =>
            {
                // 即五行生旺墓三宫之神。
                var 日支 = 式.年月日时.日支;
                return 日支.取所在三合局().ToArray();
            };
            public static 取多神煞法 三刑 => (式) =>
            {
                // 寅刑巳，巳刑申，申刑寅；丑刑戌，戌刑未，未刑丑，此为月刑。
                // 子刑卯，卯刑子，此为互刑。
                // 辰刑辰，亥刑亥，酉刑酉，午刑午，此为自刑。
                var 日支 = 式.年月日时.日支;
                return (new[] { 日支.取所刑(), 日支.取被所刑() }).Distinct().ToArray();
            };
            public static 取神煞法 六害 => (式) =>
            {
                // 子未害，午丑害，寅巳害，卯辰害，申亥害，酉戌害。
                var 日支 = 式.年月日时.日支;
                return 日支.取害();
            };
            */
            public static 取神煞法 支墓 => (式) => {
                // 用五行起例。
                var 日支 = 式.年月日时.日支;
                return 日支.五行().以长生取支(十二长生.墓);
            };
            public static 取神煞法 支破 => (式) => {
                // 阳支后三神，阴支前三神。
                // 子 丑 寅 卯 辰 巳 午 未 申 酉 戌 亥
                // 戌 卯 子 丑 寅 未 辰 酉 午 亥 申 丑
                var 日支 = 式.年月日时.日支;
                return 日支.取破();
            };
            public static 取神煞法 华盖 => (式) => {
                // 三合之第三位神。
                var 日支 = 式.年月日时.日支;
                return 日支.取所在三合局().墓支;
            };
            public static 取神煞法 将星 => (式) => {
                // 三合之第二位神。
                var 日支 = 式.年月日时.日支;
                return 日支.取所在三合局().帝旺支;
            };
            public static 取神煞法 驿马 => (式) => {
                // 三合第一位冲神。
                var 日支 = 式.年月日时.日支;
                return 马(日支);
            };
            public static 取神煞法 劫煞 => (式) => {
                // 三合第三位之前一位神。
                var 日支 = 式.年月日时.日支;
                return 日支.取所在三合局().墓支.Next();
            };
            public static 取神煞法 破碎 => (式) => {
                // 孟日用酉，仲日用巳，季日用丑。即金神，又名红砂。
                var 日支 = 式.年月日时.日支;
                return new(日支.获取孟仲季() switch {
                    孟仲季.孟 => 10,
                    孟仲季.仲 => 6,
                    _ => 2,
                });
            };
            public static 取神煞法 支仪 => (式) => {
                // 午 辰 寅 未 酉 亥
                // 巳 卯 丑 申 戌 子
                var 日支 = 式.年月日时.日支;
                return new(日支.Index switch {
                    1 => 7,
                    2 => 6,
                    3 => 5,
                    4 => 3,
                    5 => 2,
                    6 => 8,
                    7 => 9,
                    8 => 10,
                    9 => 11,
                    10 => 12,
                    _ => 1
                });
            };
            #endregion

            #region 月煞
            public static 取神煞法 天德 => (式) => {
                // 正 二 三 四 五 六 七 八 九 十 十一 十二
                // 丁 坤 壬 辛 干 甲 癸 艮 丙 乙 巽  庚
                var 月支 = 式.年月日时.月支;
                return new(月支.Index switch {
                    3 => 8,
                    4 => 9,
                    5 => 12,
                    6 => 11,
                    7 => 12,
                    8 => 3,
                    9 => 2,
                    10 => 3,
                    11 => 6,
                    12 => 5,
                    1 => 6,
                    _ => 9
                });
            };
            public static 取神煞法 月德 => (式) => {
                // 巳寅亥申三轮，即三合之禄神。
                var 月支 = 式.年月日时.月支;
                return 月支.取所在三合局().合化五行.以长生取支(十二长生.临官);
            };
            public static 取神煞法 生气 => (式) => {
                // 正月起子顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 1, 月支);
            };
            public static 取神煞法 死气 => (式) => {
                // 正月起午顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 7, 月支);
            };
            public static 取神煞法 官符 => (式) => {
                // 官符、孝服、谩语：俱同死气。
                return 死气(式);
            };
            public static 取神煞法 孝服 => (式) => {
                return 死气(式);
            };
            public static 取神煞法 谩语 => (式) => {
                return 死气(式);
            };
            public static 取神煞法 死神 => (式) => {
                // 正月起巳顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 6, 月支);
            };
            public static 取神煞法 火烛 => (式) => {
                // 同死神。
                return 死神(式);
            };
            public static 取神煞法 天医 => (式) => {
                // 正月起辰顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 5, 月支);
            };
            public static 取神煞法 地医 => (式) => {
                // 正月起戌顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 11, 月支);
            };
            public static 取神煞法 天诏 => (式) => {
                // 正月起亥顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 12, 月支);
            };
            public static 取神煞法 飞魂 => (式) => {
                // 同天诏。
                return 天诏(式);
            };
            public static 取神煞法 信神 => (式) => {
                // 正月起酉顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 10, 月支);
            };
            public static 取神煞法 血支 => (式) => {
                // 正月起丑顺行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 2, 月支);
            };
            public static 取神煞法 坑煞 => (式) => {
                // 同血支。
                return 血支(式);
            };
            public static 取神煞法 风煞 => (式) => {
                // 正月起寅逆行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 3, 月支, -1);
            };
            public static 取神煞法 风伯 => (式) => {
                // 正月起申逆行，天解同。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 9, 月支, -1);
            };
            public static 取神煞法 天解 => (式) => {
                return 风伯(式);
            };
            public static 取神煞法 月厌 => (式) => {
                // 正月起戌逆行，对宫即厌对。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 11, 月支, -1);
            };
            public static 取神煞法 厌对 => (式) => {
                var 厌 = 月厌(式);
                Debug.Assert(厌.HasValue);
                return 厌.Value.取冲();
            };
            public static 取神煞法 火光 => (式) => {
                // 同月厌。
                return 月厌(式);
            };
            public static 取神煞法 烛命 => (式) => {
                // 正月起卯逆行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 4, 月支, -1);
            };
            public static 取神煞法 天鸡 => (式) => {
                // 正月起酉逆行。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(3, 10, 月支, -1);
            };
            public static 取神煞法 天马 => (式) => {
                // 正七月起午，顺行六阳。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(9, 7, 月支, 2);
            };
            public static 取神煞法 皇恩 => (式) => {
                // 正七月起未，顺行六阴。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(9, 8, 月支, 2);
            };
            public static 取神煞法 天财 => (式) => {
                // 正七月起辰，顺行六阳。
                var 月支 = 式.年月日时.月支;
                return 某日月起某某行十二支(9, 5, 月支, 2);
            };
            public static 取神煞法 血忌 => (式) => {
                // 阳月起丑顺行，阴月起未顺行。
                var 月支 = 式.年月日时.月支;
                return new(月支.Index switch {
                    3 => 2,
                    5 => 3,
                    7 => 4,
                    9 => 5,
                    11 => 6,
                    1 => 7,
                    4 => 8,
                    6 => 9,
                    8 => 10,
                    10 => 11,
                    12 => 12,
                    _ => 1,
                });
            };
            public static 取神煞法 飞廉 => (式) => {
                // 卯月起巳，午月起寅，酉月起亥，子月起申，俱顺行。
                var 月支 = 式.年月日时.月支;
                return new(月支.Index switch {
                    4 => 6,
                    5 => 7,
                    6 => 8,
                    7 => 3,
                    8 => 4,
                    9 => 5,
                    10 => 12,
                    11 => 1,
                    12 => 2,
                    1 => 9,
                    2 => 10,
                    _ => 11,
                });
            };
            public static 取神煞法 勾神 => (式) => {
                // 阳月起卯，隔月顺行六阴神。阴月起戌，隔月顺行六阳神。
                var 月支 = 式.年月日时.月支;
                return new(月支.Index switch {
                    3 => 4,
                    5 => 6,
                    7 => 8,
                    9 => 10,
                    11 => 12,
                    1 => 2,
                    4 => 11,
                    6 => 1,
                    8 => 3,
                    10 => 5,
                    12 => 7,
                    _ => 9
                });
            };
            public static 取神煞法 绞神 => (式) => {
                // 勾神对宫。
                var 勾 = 勾神(式);
                Debug.Assert(勾.HasValue);
                return 勾.Value.取冲();
            };
            public static 取神煞法 会神 => (式) => {
                // 未 寅 酉 丑 巳 申
                // 戌 亥 子 午 卯 辰
                var 月支 = 式.年月日时.月支;
                return new(月支.Index switch {
                    3 => 8,
                    4 => 11,
                    5 => 3,
                    6 => 12,
                    7 => 10,
                    8 => 1,
                    9 => 2,
                    10 => 7,
                    11 => 6,
                    12 => 4,
                    1 => 9,
                    _ => 5
                });
            };
            public static 取神煞法 成神 => (式) => {
                // 驿马合神。如正五九月马在申，巳与申合即是。
                var 月支 = 式.年月日时.月支;
                return 马(月支).取六合();
            };
            public static 取神煞法 天鬼 => (式) => {
                // 驿马前一位神。
                var 月支 = 式.年月日时.月支;
                return 马(月支).Next();
            };
            public static 取神煞法 悬索 => (式) => {
                // 天鬼对宫。
                var 鬼 = 天鬼(式);
                Debug.Assert(鬼.HasValue);
                return 鬼.Value.取冲();
            };
            public static 取神煞法 桃花 => (式) => {
                // 同悬索。
                return 悬索(式);
            };
            public static 取神煞法 产煞 => (式) => {
                // 阳月用驿马，阴月用马对宫。
                var 月支 = 式.年月日时.月支;
                return 月支.阴阳().IsYang ? 马(月支) : 马(月支).取冲();
            };
            public static 取神煞法 大煞 => (式) => {
                // 月德前一位。
                var 德 = 月德(式);
                Debug.Assert(德.HasValue);
                return 德.Value.Next();
            };
            public static 取神煞法 丧魄 => (式) => {
                // 月德前二位。
                var 德 = 月德(式);
                Debug.Assert(德.HasValue);
                return 德.Value.Next(2);
            };
            #endregion

            #region 旬煞
            public static 取神煞法 三奇 => (式) => {
                // 甲子甲戌旬在丑，甲申甲午旬在子，甲辰甲寅旬在亥。
                var 旬 = 式.年月日时.旬所在;
                return new(旬.旬首支.Index switch {
                    1 or 11 => 2,
                    9 or 7 => 1,
                    5 or 3 => 12,
                    _ => throw new 起课失败异常("错误的年月日时。")
                });
            };
            public static 取神煞法 六仪 => (式) => {
                // 旬首之神。
                var 旬 = 式.年月日时.旬所在;
                return 旬.旬首支;
            };
            public static 取神煞法 丁马 => (式) => {
                // 六丁之神。
                var 旬 = 式.年月日时.旬所在;
                return 旬.获取对应地支(new HeavenlyStem(4));
            };
            public static 取多神煞法 旬空 => (式) => {
                // 十干不到之处。
                var 旬 = 式.年月日时.旬所在;
                return new[] { 旬.空亡一, 旬.空亡二 };
            };
            #endregion

            #region 时煞
            public static 取神煞法 天赦 => (式) => {
                // 戊寅、甲午、戊申、甲子。
#warning 此戊、甲为何意？
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 3,
                    四时.夏 => 7,
                    四时.秋 => 9,
                    _ => 1,
                });
            };
            public static 取神煞法 皇书 => (式) => {
                // 四时临官之神，如春木临官在寅之类。
                var 时 = 式.年月日时.月支.取所在四时局();
                return 时.五行.以长生取支(十二长生.临官);
            };
            public static 取神煞法 孤辰 => (式) => {
                // 四时前一位。
                var 时 = 式.年月日时.月支.取所在四时局();
                return 时.季支.Next();
            };
            public static 取神煞法 寡宿 => (式) => {
                // 四时后一位，关神同。
                var 时 = 式.年月日时.月支.取所在四时局();
                return 时.孟支.Next(-1);
            };
            public static 取神煞法 关神 => (式) => {
                return 寡宿(式);
            };
            public static 取神煞法 喝散 => (式) => {
                // 喝散、钥神：同孤辰。
                return 孤辰(式);
            };
            public static 取神煞法 钥神 => (式) => {
                return 孤辰(式);
            };
            public static 取神煞法 火鬼 => (式) => {
                // 午酉子卯。
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 7,
                    四时.夏 => 10,
                    四时.秋 => 1,
                    _ => 4,
                });
            };
            public static 取神煞法 丧车 => (式) => {
                // 天喜后一位。
                var 喜 = 天喜(式);
                Debug.Assert(喜.HasValue);
                return 喜.Value.Next(-1);
            };
            public static 取神煞法 天喜 => (式) => {
                // 戌丑辰未。
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 11,
                    四时.夏 => 2,
                    四时.秋 => 5,
                    _ => 8,
                });
            };
            public static 取神煞法 天耳 => (式) => {
                // 同天喜。
                return 天喜(式);
            };
            public static 取神煞法 浴盆 => (式) => {
                // 天喜冲位。
                var 喜 = 天喜(式);
                Debug.Assert(喜.HasValue);
                return 喜.Value.取冲();
            };
            public static 取神煞法 天目 => (式) => {
                // 同浴盆。
                return 浴盆(式);
            };
            public static 取神煞法 哭神 => (式) => {
                // 未戌丑辰。
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 8,
                    四时.夏 => 11,
                    四时.秋 => 2,
                    _ => 5,
                });
            };
            public static 取神煞法 五墓 => (式) => {
                // 同哭神。
                return 哭神(式);
            };
            public static 取神煞法 游神 => (式) => {
                // 丑子亥戌。
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 2,
                    四时.夏 => 1,
                    四时.秋 => 12,
                    _ => 11,
                });
            };
            public static 取神煞法 戏神 => (式) => {
                // 巳子酉辰。
                var 时 = 式.年月日时.月支.获取四时();
                return new(时 switch {
                    四时.春 => 6,
                    四时.夏 => 1,
                    四时.秋 => 10,
                    _ => 5,
                });
            };
            #endregion

            #region 岁煞
            public static 取神煞法 大耗 => (式) => {
                // 岁后一位。
                var 年支 = 式.年月日时.年支;
                return 年支.Next(-1);
            };
            public static 取神煞法 丧门 => (式) => {
                // 岁前二位。
                var 年支 = 式.年月日时.年支;
                return 年支.Next(2);
            };
            public static 取神煞法 吊客 => (式) => {
                // 岁后二位。
                var 年支 = 式.年月日时.年支;
                return 年支.Next(-2);
            };
            public static 取神煞法 岁墓 => (式) => {
                // 岁后五位。
                var 年支 = 式.年月日时.年支;
                return 年支.Next(-5);
            };
            public static 取神煞法 岁虎 => (式) => {
                // 岁墓前一位。
                var 墓 = 岁墓(式);
                Debug.Assert(墓.HasValue);
                return 墓.Value.Next(1);
            };
            #endregion
            #endregion 各神煞
        }

        private static readonly Dictionary<string, 取神煞法> 取神煞法列表 = new();

        private static readonly Dictionary<string, 取多神煞法> 取多神煞法列表 = new();

        static 神煞六壬辨疑神煞纪要实现()
        {
            foreach (var p in typeof(取神煞方法).GetProperties())
            {
                var r = p.GetValue(null);
                Debug.Assert(r is 取神煞法 or 取多神煞法);
                if (r is 取神煞法 单)
                    取神煞法列表.Add(p.Name, 单);
                else if (r is 取多神煞法 多)
                    取多神煞法列表.Add(p.Name, 多);
            }
        }

        private sealed record 神煞题目(
            string 神煞名) : I神煞题目
        { }
        private sealed record 神煞内容(
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞内容
        { }

        public static IEnumerable<I神煞题目> 支持的神煞 =>
            取神煞法列表.Keys.Concat(取多神煞法列表.Keys)
            .Select(神煞名 => new 神煞题目(神煞名));

        public static I神煞内容 取煞(I年月日时 年月日时, string 神煞名)
        {
            壬式 式 = new(年月日时);
            if (取神煞法列表.TryGetValue(神煞名, out var 取单法))
            {
                var 结果 = 取单法(式);
                if (结果.HasValue)
                    return new 神煞内容(Array.AsReadOnly(new[] { 结果.Value }));
                else
                    return new 神煞内容(Array.Empty<EarthlyBranch>());
            }

            if (取多神煞法列表.TryGetValue(神煞名, out var 取多法))
            {
                var 结果 = 取多法(式);
                return new 神煞内容(Array.AsReadOnly(结果));
            }

            throw new 起课失败异常($"不支持的神煞题目：{神煞名}");
        }
    }
}
