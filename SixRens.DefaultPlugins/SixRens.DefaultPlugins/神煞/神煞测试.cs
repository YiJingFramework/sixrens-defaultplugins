using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.神煞
{
    public sealed class 神煞测试 : I神煞插件
    {
        public string? 插件名 => "测试神煞";

        public Guid 插件识别码 => new Guid("59375F38-6BE3-46AC-8C46-F4BBF0C03382");

        public IEnumerable<I神煞> 获取神煞(I年月日时 年月日时, 地支盘 基础盘, 地支盘 地盘, 地支盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命)
        {
            return new 神煞测试实现(年月日时).取煞();
        }
    }
}
