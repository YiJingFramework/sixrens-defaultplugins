using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.天盘
{
    public sealed class 天盘月将加时 : I天盘插件
    {
        public string? 插件名 => "月将加时";

        public Guid 插件识别码 => new Guid("7D33B8B0-5CC2-4E78-AB44-077C69908249");

        public I天盘 获取天盘(I年月日时 年月日时, I地盘 地盘)
        {
            return new 天盘月将加时默认实现(年月日时);
        }
    }
}
