using SixRens.Api.工具;
using YiJingFramework.Core;
using YiJingFramework.FiveElements;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.三传
{
    internal partial class 三传涉害深浅计算式实现
    {
        private sealed class 四课之一
        {
            internal HeavenlyStem? 干下 { get; }
            internal EarthlyBranch 支下或干下之寄宫 { get; }
            internal FiveElement 下五行 { get; }
            internal YinYang 下阴阳 { get; }
            internal EarthlyBranch 上 { get; }
            internal FiveElement 上五行 { get; }
            internal YinYang 上阴阳 { get; }
            internal 四课之一(EarthlyBranch 下, EarthlyBranch 上)
            {
                this.上 = 上;
                this.上五行 = 上.五行();
                this.上阴阳 = 上.阴阳();

                this.干下 = null;
                this.支下或干下之寄宫 = 下;
                this.下五行 = 下.五行();
                this.下阴阳 = 下.阴阳();
            }
            internal 四课之一(HeavenlyStem 下, EarthlyBranch 上)
            {
                this.上 = 上;
                this.上五行 = 上.五行();
                this.上阴阳 = 上.阴阳();

                this.干下 = 下;
                this.支下或干下之寄宫 = 下.寄宫();
                this.下五行 = 下.五行();
                this.下阴阳 = 下.阴阳();
            }
        }
    }
}
