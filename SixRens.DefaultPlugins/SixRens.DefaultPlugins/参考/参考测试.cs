using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.参考
{
    public sealed class 参考测试 : I参考插件
    {
        public string? 插件名 => "测试参考";

        public Guid 插件识别码 => new Guid("76C9F1AA-6E3E-40CB-90C6-A3B146529043");

        private record 占断参考题目(
            string 题目) : I占断参考题目
        { }

        private record 占断参考内容(
            string? 内容) : I占断参考内容
        { }

        private static readonly Dictionary<string, string> 参考字典 = new()
        {
            { "参考测试一", "这是一个测试参考" },
            { "参考测试二", "这是第二个测试参考" },
        };

        public IEnumerable<I占断参考题目> 支持的占断参考
        {
            get
            {
                return 参考字典.Keys.Select(题目 => new 占断参考题目(题目));
            }
        }

        public I占断参考内容 生成占断参考(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表, IReadOnlyList<I课体> 课体列表, string 占断参考题目)
        {
            if (!参考字典.TryGetValue(占断参考题目, out var 内容))
                throw new 起课失败异常($"不支持的占断参考题目：{占断参考题目}");
            return new 占断参考内容(内容);
        }
    }
}
