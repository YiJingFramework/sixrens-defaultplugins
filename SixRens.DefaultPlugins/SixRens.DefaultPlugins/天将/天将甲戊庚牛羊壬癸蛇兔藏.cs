using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;

namespace SixRens.DefaultPlugins.天将
{
    public sealed class 天将甲戊庚牛羊壬癸蛇兔藏 : I天将插件
    {
        public I天将盘 获取天将盘(地支盘 基础盘, I年月日时 年月日时, 地支盘 地盘, 地支盘 天盘, I四课 四课, I三传 三传)
        {
            return new 天将甲戊庚牛羊壬癸蛇兔藏实现(年月日时, new 天地盘(地盘, 天盘));
        }
    }
}
