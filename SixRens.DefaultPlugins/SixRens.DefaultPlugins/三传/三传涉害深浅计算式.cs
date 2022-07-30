using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using static SixRens.Api.工具.天盘扩展;

namespace SixRens.DefaultPlugins.三传
{
    public sealed class 三传涉害深浅计算式 : I三传插件
    {
        public string? 插件名 => "涉害深浅计算";

        public Guid 插件识别码 => new Guid("F53253DC-099B-412A-8B14-6C59BE64DA38");

        public I三传 获取三传(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课)
        {
            return new 三传涉害深浅计算式实现(四课, 天盘.完全可逆化(true));
        }
    }
}
