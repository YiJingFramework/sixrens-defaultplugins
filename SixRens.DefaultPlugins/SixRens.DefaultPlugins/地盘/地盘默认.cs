using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.地盘
{
    public sealed class 地盘默认 : I地盘插件
    {
        public string? 插件名 => "默认";

        public Guid 插件识别码 => new Guid("A9B377C9-8A25-4476-95C9-7BA90D075A5E");

        public 地支盘 获取地盘(地支盘 基础盘, I年月日时 年月日时)
        {
            return 基础盘;
        }
    }
}
