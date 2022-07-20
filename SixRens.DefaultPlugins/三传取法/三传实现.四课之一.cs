using SixRens.Api.工具;
using YiJingFramework.Core;
using YiJingFramework.FiveElements;
using YiJingFramework.StemsAndBranches;

namespace 三传取法
{
    internal partial class 三传实现
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
                上 = 下上.上;
                上五行 = 下上.上.五行();
                上阴阳 = 下上.上.阴阳();

                干下 = null;
                支下或干下之寄宫 = 下上.下;
                下五行 = 下上.下.五行();
                下阴阳 = 下上.下.阴阳();
            }
            internal 四课之一((HeavenlyStem 下, EarthlyBranch 上) 下上)
            {
                上 = 下上.上;
                上五行 = 下上.上.五行();
                上阴阳 = 下上.上.阴阳();

                干下 = 下上.下;
                支下或干下之寄宫 = 下上.下.寄宫();
                下五行 = 下上.下.五行();
                下阴阳 = 下上.下.阴阳();
            }
        }
    }
}
