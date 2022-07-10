using SixRens.Api.工具;
using YiJingFramework.Core;
using YiJingFramework.FiveElements;
using YiJingFramework.StemsAndBranches;

namespace SixRens.式.三传实现
{
    internal partial class 三传依深浅实现计算式
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
            internal 四课之一((EarthlyBranch 下, EarthlyBranch 上) 下上)
            {
                this.上 = 下上.上;
                this.上五行 = 下上.上.五行();
                this.上阴阳 = 下上.上.阴阳();

                this.干下 = null;
                this.支下或干下之寄宫 = 下上.下;
                this.下五行 = 下上.下.五行();
                this.下阴阳 = 下上.下.阴阳();
            }
            internal 四课之一((HeavenlyStem 下, EarthlyBranch 上) 下上)
            {
                this.上 = 下上.上;
                this.上五行 = 下上.上.五行();
                this.上阴阳 = 下上.上.阴阳();

                this.干下 = 下上.下;
                this.支下或干下之寄宫 = 下上.下.寄宫();
                this.下五行 = 下上.下.五行();
                this.下阴阳 = 下上.下.阴阳();
            }
        }
    }
}
