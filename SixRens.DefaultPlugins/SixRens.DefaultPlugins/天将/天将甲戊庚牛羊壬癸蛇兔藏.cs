using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;

namespace SixRens.DefaultPlugins.天将
{
    public sealed class 天将甲戊庚牛羊壬癸蛇兔藏 : I天将插件
    {
        public string? 插件名 => "甲戊庚牛羊（壬癸蛇兔藏）";

        public Guid 插件识别码 => new Guid("006CD940-0597-4E02-A707-0D54D4216C1A");

        public I天将盘 获取天将盘(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传)
        {
            return new 天将甲戊庚牛羊壬癸蛇兔藏实现(年月日时, new 可逆天盘(天盘));
        }
    }
}
