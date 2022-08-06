using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.参考
{
    public sealed class 参考观易吟 : I参考插件
    {
        public string? 插件名 => "观易吟";

        public Guid 插件识别码 { get; } = new Guid("76C9F1AA-6E3E-40CB-90C6-A3B146529043");

        private record 占断参考(
            EarthlyBranch? 相关宫位, string 题目, string 内容) : I占断参考
        { }

        public IEnumerable<I占断参考> 生成占断参考(I起课信息 起课信息, I天地盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, IReadOnlyList<I神煞> 神煞列表, IReadOnlyList<I课体> 课体列表)
        {
            yield return new 占断参考(
                null, "观易吟",
                "一物从来有一身一身还有一乾坤" +
                "能知万物备于我肯把三才别立根" +
                "天心一中分造化人于心上起经纶" +
                "仙人亦有两般话道不虚行只在人");
        }
    }
}
