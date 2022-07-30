using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace 三传取法
{
    public sealed class 三传插件 : I三传插件
    {
        public string? 插件名 => "根据深度";

        public Guid 插件识别码 => new Guid("4227FB82-4106-4FBB-B017-19183AB22C4E");

        public I三传 获取三传(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课)
        {
            return new 三传实现(四课, new 可逆天盘(天盘));
        }
    }
}
