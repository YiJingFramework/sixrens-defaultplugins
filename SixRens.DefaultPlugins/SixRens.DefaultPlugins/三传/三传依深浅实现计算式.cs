using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;
using YiJingFramework.StemsAndBranches;

namespace SixRens.式.三传实现
{
    /// <summary>
    /// 此二日起得结果不正确：
    /// 甲子 子 癸酉 子 辰
    /// 甲子 子 己卯 子 辰
    /// </summary>
    [Obsolete]
    internal sealed partial class 三传依深浅实现计算式 : I三传
    {
        private readonly I四课 四课;
        private readonly 天地盘 天地盘;
        internal 三传依深浅实现计算式(I四课 四课, 天地盘 天地盘, bool 立即计算 = true)
        {
            this.四课 = 四课;
            this.天地盘 = 天地盘;
            if (立即计算)
                this.重新计算();
        }

        public EarthlyBranch 初传 { get; private set; }

        public EarthlyBranch 中传 { get; private set; }

        public EarthlyBranch 末传 { get; private set; }
    }
}
