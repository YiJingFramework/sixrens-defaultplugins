using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.参考
{
    public sealed class 参考测试 : I参考插件
    {
        public string? 插件名 => "测试参考";

        public Guid 插件识别码 => new Guid("76C9F1AA-6E3E-40CB-90C6-A3B146529043");

        public IEnumerable<I占断参考> 生成占断参考(I年月日时 年月日时, 地支盘 基础盘, 地支盘 地盘, 地支盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表, IReadOnlyList<I课体> 课体列表)
        {
            return new 参考测试实现().取参考();
        }
    }
}
