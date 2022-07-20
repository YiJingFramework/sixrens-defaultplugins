using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 三传取法
{
    internal class 可逆天盘
    {
        public I天盘 天盘 { get; }

        private readonly Dictionary<EarthlyBranch, EarthlyBranch> 从天神查地支表;

        public 可逆天盘(I天盘 天盘)
        {
            this.天盘 = 天盘;

            this.从天神查地支表 = new Dictionary<EarthlyBranch, EarthlyBranch>(12);
            for (int i = 1; i <= 12; i++)
            {
                var 地支 = new EarthlyBranch(i);
                var 天神 = this.天盘.取天神(地支);
                this.从天神查地支表.Add(天神, 地支);
            }
        }

        public EarthlyBranch 取所乘神(EarthlyBranch 地盘支)
        {
            return 天盘.取天神(地盘支);
        }
        public EarthlyBranch 取所临神(EarthlyBranch 天神)
        {
            return this.从天神查地支表[天神];
        }
    }
}
