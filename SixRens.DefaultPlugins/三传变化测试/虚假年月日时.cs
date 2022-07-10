using com.nlf.calendar;
using SixRens.Api.实体;
using YiJingFramework.StemsAndBranches;

namespace 三传变化测试
{
    internal class 虚假年月日时 : I年月日时
    {
        public class 旬 : I年月日时.I旬
        {
            public HeavenlyStem 旬首干 => new HeavenlyStem(1);
            public 旬((HeavenlyStem 干, EarthlyBranch 支) 旬内一日)
            {
                this.旬首支 = new EarthlyBranch(旬内一日.支.Index - 旬内一日.干.Index + 1);
            }
            public EarthlyBranch 旬首支 { get; }
            public EarthlyBranch 空亡一 => new EarthlyBranch(this.旬首支.Index - 2);
            public EarthlyBranch 空亡二 => new EarthlyBranch(this.旬首支.Index - 1);
            public IEnumerable<(HeavenlyStem 干, EarthlyBranch 支)> 每日干支
            {
                get
                {
                    HeavenlyStem 干 = this.旬首干;
                    EarthlyBranch 支 = this.旬首支;
                    for (; 干.Index <= 10;)
                    {
                        yield return (干, 支);
                        干 = new HeavenlyStem(干.Index + 1);
                        支 = new EarthlyBranch(支.Index + 1);
                    }
                }
            }

            public HeavenlyStem? 获取对应天干(EarthlyBranch 地支)
            {
                if (地支 == this.空亡一 || 地支 == this.空亡二)
                    return null;
                return new HeavenlyStem((地支.Index - this.旬首支.Index + 13) % 12);
            }
            public EarthlyBranch 获取对应地支(HeavenlyStem 天干)
            {
                return new EarthlyBranch(天干.Index - 1 + this.旬首支.Index);
            }
        }

        public HeavenlyStem 年干 { get; }
        public HeavenlyStem 月干 { get; }
        public HeavenlyStem 日干 { get; }
        public HeavenlyStem 时干 { get; }
        public EarthlyBranch 年支 { get; }
        public EarthlyBranch 月支 { get; }
        public EarthlyBranch 日支 { get; }
        public EarthlyBranch 时支 { get; }
        public bool 昼占 { get; }
        public I年月日时.I旬 旬所在 { get; }
        public EarthlyBranch 月将 { get; }

        public 虚假年月日时(Lunar lunar, EarthlyBranch 月将)
        {
            this.年干 = 天干表[lunar.getYearGanByLiChun()];
            this.月干 = 天干表[lunar.getMonthGan()];
            this.日干 = 天干表[lunar.getDayGanExact()];
            this.时干 = 天干表[lunar.getTimeGan()];

            this.年支 = 地支表[lunar.getYearZhiByLiChun()];
            this.月支 = 地支表[lunar.getMonthZhi()];
            this.日支 = 地支表[lunar.getDayZhiExact()];
            this.时支 = 地支表[lunar.getTimeZhi()];

            this.昼占 = this.时支.Index is >= 4 and < 10;

            this.旬所在 = new 旬((this.日干, this.日支));

            // lunar = Lunar.fromDate(dateTime.Date.AddDays(1));
            this.月将 = 月将;
        }

        public static IReadOnlyDictionary<string, HeavenlyStem> 天干表 { get; }
            = HeavenlyStem.BuildStringStemTable("C").ToDictionary(item => item.s, item => item.stem);
        public static IReadOnlyDictionary<string, EarthlyBranch> 地支表 { get; }
            = EarthlyBranch.BuildStringBranchTable("C").ToDictionary(item => item.s, item => item.branch);
    }
}
