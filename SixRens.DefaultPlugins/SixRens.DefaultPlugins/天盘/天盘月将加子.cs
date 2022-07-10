using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.天盘
{
    public sealed class 天盘月将加子 : I天盘插件
    {
        public 地支盘 获取天盘(地支盘 基础盘, I年月日时 年月日时, 地支盘 地盘)
        {
            return new 地支盘(地盘, 年月日时.时支, 年月日时.月将);
        }
    }
}
