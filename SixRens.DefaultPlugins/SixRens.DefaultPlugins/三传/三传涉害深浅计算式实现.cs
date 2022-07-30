using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.工具.天盘扩展;

namespace SixRens.DefaultPlugins.三传
{
    internal sealed partial class 三传涉害深浅计算式实现 : I三传
    {
        private readonly I四课 四课;
        private readonly 完全可逆天盘 天地盘;
        internal 三传涉害深浅计算式实现(I四课 四课, 完全可逆天盘 天地盘)
        {
            this.四课 = 四课;
            this.天地盘 = 天地盘;
            this.重新计算();
        }

        public EarthlyBranch 初传 { get; private set; }

        public EarthlyBranch 中传 { get; private set; }

        public EarthlyBranch 末传 { get; private set; }
    }
}
