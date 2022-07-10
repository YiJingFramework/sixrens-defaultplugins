using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.神煞
{
    public sealed class 神煞示例 : I神煞插件
    {
        public I神煞表 获取神煞表(地支盘 基础盘, I年月日时 年月日时, 地支盘 地盘, 地支盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘)
        {
            return new 神煞示例实现(年月日时);
        }
    }
}
