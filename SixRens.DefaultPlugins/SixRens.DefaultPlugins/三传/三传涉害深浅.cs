using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;
using SixRens.式.三传实现;

namespace SixRens.DefaultPlugins.三传
{
    public sealed class 三传涉害深浅 : I三传插件
    {
        public string? 插件名 => "涉害深浅";

        public Guid 插件识别码 => new Guid("620CC386-F3C3-4658-ADED-F1A9E4903B9F");

        public I三传 获取三传(地支盘 基础盘, I年月日时 年月日时, 地支盘 地盘, 地支盘 天盘, I四课 四课)
        {
            return new 三传涉害深浅实现打表式(四课, new 天地盘(地盘, 天盘));
        }
    }
}
