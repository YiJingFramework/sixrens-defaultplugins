using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.课体
{
    public sealed class 课体测试 : I课体插件
    {
        public string? 插件名 => "测试课体";

        public Guid 插件识别码 { get; } = new Guid("ABDFB6DD-90B9-4B4A-A8A4-4D60996C948D");

        private readonly static Dictionary<string, Func<I年月日时, bool>> 测试课体表 = new() {
            { "测试一", (年月日时) => true },
            { "测试昼占", (年月日时) => 年月日时.昼占 },
            { "测试夜占", (年月日时) => !年月日时.昼占 },
        };

        public IEnumerable<string> 支持的课体 => 测试课体表.Keys;


        public I课体插件.I课体内容提供器 获取课体内容提供器(I起课信息 起课信息, I天地盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, IReadOnlyList<I神煞> 神煞列表)
        {
            return new 课体测试实现(起课信息.年月日时);
        }
    }
}
