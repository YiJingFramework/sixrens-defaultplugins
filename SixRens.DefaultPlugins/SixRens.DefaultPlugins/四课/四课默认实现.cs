using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.工具;
using SixRens.DefaultPlugins.工具;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.四课
{
    internal sealed class 四课默认实现 : I四课
    {
        internal 四课默认实现(I年月日时 年月日时, 天地盘 天地盘)
        {
            {
                var 日 = 年月日时.日干;
                var 日阳 = 天地盘.取所乘神(日.寄宫());
                var 日阴 = 天地盘.取所乘神(日阳);

                this.日 = 日;
                this.日阳 = 日阳;
                this.日阴 = 日阴;
            }

            {
                var 辰 = 年月日时.日支;
                var 辰阳 = 天地盘.取所乘神(辰);
                var 辰阴 = 天地盘.取所乘神(辰阳);

                this.辰 = 辰;
                this.辰阳 = 辰阳;
                this.辰阴 = 辰阴;
            }
        }

        public HeavenlyStem 日 { get; private set; }

        public EarthlyBranch 日阳 { get; private set; }

        public EarthlyBranch 日阴 { get; private set; }

        public EarthlyBranch 辰 { get; private set; }

        public EarthlyBranch 辰阳 { get; private set; }

        public EarthlyBranch 辰阴 { get; private set; }
    }
}
