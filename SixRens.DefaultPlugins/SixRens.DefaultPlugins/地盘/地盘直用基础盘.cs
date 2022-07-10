using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.地盘
{
    public sealed class 地盘直用基础盘 : I地盘插件
    {
        public 地支盘 获取地盘(地支盘 基础盘, I年月日时 年月日时)
        {
            return 基础盘;
        }
    }
}
