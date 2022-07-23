using SixRens.Api.实体;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.年命
{
    internal sealed class 年命默认实现 : I年命
    {
        public 年命默认实现(I年月日时 年月日时, YinYang 性别, EarthlyBranch 本命)
        {
            this.性别 = 性别;
            this.本命 = 本命;

            if (性别.IsYang)
            {
                const int 寅数 = 3;
                this.行年 = new(寅数 + 年月日时.年支.Index - 本命.Index);
            }
            else
            {
                const int 申数 = 9;
                this.行年 = new(申数 - 年月日时.年支.Index + 本命.Index);
            }
        }

        public YinYang 性别 { get; }

        public EarthlyBranch 本命 { get; }

        public EarthlyBranch 行年 { get; }
    }
}
