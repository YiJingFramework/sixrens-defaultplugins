using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.式.三传实现
{
    internal sealed partial class 三传涉害深浅实现打表式 : I三传
    {
        private static readonly EarthlyBranch 子 = new EarthlyBranch(1);

        internal 三传涉害深浅实现打表式(I四课 四课, 天地盘 天地盘)
        {
            var 键 = 生成键(四课.日, 四课.辰, 天地盘.取所乘神(子));
            var (初, 中, 末) = 获取三传(键);

            Debug.Assert(初 is not 0 && 中 is not 0 && 末 is not 0);

            this.初传 = new EarthlyBranch(初);
            this.中传 = new EarthlyBranch(中);
            this.末传 = new EarthlyBranch(末);
        }

        public EarthlyBranch 初传 { get; private set; }

        public EarthlyBranch 中传 { get; private set; }

        public EarthlyBranch 末传 { get; private set; }
    }
}
