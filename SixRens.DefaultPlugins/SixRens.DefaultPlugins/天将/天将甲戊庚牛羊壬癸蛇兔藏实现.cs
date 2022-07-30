using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.工具.天盘扩展;

namespace SixRens.DefaultPlugins.天将
{
    internal sealed class 天将甲戊庚牛羊壬癸蛇兔藏实现 : I天将盘
    {
        internal 天将甲戊庚牛羊壬癸蛇兔藏实现(I年月日时 年月日时, 完全可逆天盘 天地盘)
        {
            this.贵人序号 = 年月日时.日干.Index switch {
                // 甲戊庚牛羊
                1 or 5 or 7 => 年月日时.昼占 ? 2 : 8,
                // 乙己鼠猴乡
                2 or 6 => 年月日时.昼占 ? 1 : 9,
                // 丙丁猪鸡位
                3 or 4 => 年月日时.昼占 ? 12 : 10,
                // 壬癸蛇兔藏
                9 or 10 => 年月日时.昼占 ? 6 : 4,
                // 六辛逢马虎
                _ => 年月日时.昼占 ? 7 : 3 // 7 or 8
                // 此是贵人方
            };
            var 临神 = 天地盘.取临地(new EarthlyBranch(this.贵人序号));
            this.顺逆 = 临神.Index is >= 6 and < 12 ? -1 : 1;
        }

        private readonly int 贵人序号;
        private readonly int 顺逆;

        public Api.实体.天将 取乘将(EarthlyBranch 上者)
        {
            var d = 上者.Index - this.贵人序号;
            d = (d * this.顺逆 + 12) % 12;
            return (Api.实体.天将)d;
        }
    }
}
