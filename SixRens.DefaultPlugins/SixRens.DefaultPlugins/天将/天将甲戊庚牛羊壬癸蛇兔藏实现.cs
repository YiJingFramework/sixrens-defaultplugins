using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.天将
{
    internal sealed class 天将甲戊庚牛羊壬癸蛇兔藏实现 : I去冗天将盘
    {
        internal 天将甲戊庚牛羊壬癸蛇兔藏实现(I年月日时 年月日时, I天地盘 天地盘)
        {
            this.贵人天盘所乘 = new(年月日时.日干.Index switch {
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
            });
            var 临神 = 天地盘.取临地(this.贵人天盘所乘);
            this.为顺行 = 临神.Index is 12 or 1 or 2 or 3 or 4 or 5;
        }

        public bool 为顺行 { get; }
        public EarthlyBranch 贵人天盘所乘 { get; }
    }
}
