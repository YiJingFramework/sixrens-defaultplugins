using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.天盘
{
    internal sealed class 天盘月将加时默认实现 : I天盘
    {
        private readonly int 偏移;

        public 天盘月将加时默认实现(I年月日时 年月日时)
        {
            偏移 = 年月日时.月将.Index - 年月日时.时支.Index;
        }

        public EarthlyBranch 取天神(EarthlyBranch 地盘支)
        {
            return new EarthlyBranch(偏移 + 地盘支.Index);
        }
    }
}
