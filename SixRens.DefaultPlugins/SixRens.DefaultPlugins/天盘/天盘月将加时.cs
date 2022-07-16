using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.天盘
{
    public sealed class 天盘月将加时 : I天盘插件
    {
        public string? 插件名 => "月将加时";

        public Guid 插件识别码 => new Guid("006CD940-0597-4E02-A707-0D54D4216C1A");

        public 地支盘 获取天盘(I年月日时 年月日时, 地支盘 基础盘, 地支盘 地盘)
        {
            return new 地支盘(地盘, 年月日时.时支, 年月日时.月将);
        }
    }
}
