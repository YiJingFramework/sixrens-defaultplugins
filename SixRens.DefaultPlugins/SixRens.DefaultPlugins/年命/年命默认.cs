using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.年命
{
    public sealed class 年命默认 : I年命插件
    {
        public string? 插件名 => "默认";

        public Guid 插件识别码 => new Guid("56892793-4951-495B-98F0-F9683B3F2AF5");

        public I年命 获取年命(Guid 壬式识别码, I年月日时 年月日时,
            I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘,
            YinYang 性别, EarthlyBranch 本命)
        {
            return new 年命默认实现(年月日时, 性别, 本命);
        }
    }
}
