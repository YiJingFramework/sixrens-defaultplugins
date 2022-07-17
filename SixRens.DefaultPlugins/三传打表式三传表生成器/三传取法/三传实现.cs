using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;
using 三传打表式三传表生成器.三传取法;

namespace SixRens.式.三传实现
{
    internal sealed partial class 三传实现 : I三传
    {
        private readonly I四课 四课;
        private readonly 天地盘 天地盘;
        internal 三传实现(I四课 四课, 天地盘 天地盘, bool 立即计算 = true)
        {
            this.四课 = 四课;
            this.天地盘 = 天地盘;
            if (立即计算)
                重新计算();
        }

        public EarthlyBranch 初传 { get; private set; }

        public EarthlyBranch 中传 { get; private set; }

        public EarthlyBranch 末传 { get; private set; }
    }
}
