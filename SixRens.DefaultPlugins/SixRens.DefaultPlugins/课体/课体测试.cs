using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.课体
{
    public sealed class 课体测试 : I课体插件
    {
        public string? 插件名 => "测试课体";

        public Guid 插件识别码 => new Guid("ABDFB6DD-90B9-4B4A-A8A4-4D60996C948D");

        public IEnumerable<I课体> 识别课体(I年月日时 年月日时, 地支盘 基础盘, 地支盘 地盘, 地支盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表)
        {
            return new 课体测试实现(年月日时).取课体();
        }
    }
}
