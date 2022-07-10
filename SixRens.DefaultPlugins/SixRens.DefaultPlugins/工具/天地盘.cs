using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.工具
{
    internal class 天地盘
    {
        public 地支盘 地盘 { get; }

        public 地支盘 天盘 { get; }

        public 天地盘(地支盘 地盘, 地支盘 天盘)
        {
            this.地盘 = 地盘;
            this.天盘 = 天盘;
        }

        public EarthlyBranch 取所乘神(EarthlyBranch 下者)
        {
            return this.天盘.获取同位支(this.地盘, 下者);
        }
        public EarthlyBranch 取所临神(EarthlyBranch 上者)
        {
            return this.地盘.获取同位支(this.天盘, 上者);
        }
    }
}
