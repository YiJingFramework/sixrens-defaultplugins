using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.课体
{
    public sealed class 课体测试 : I课体插件
    {
        public string? 插件名 => "测试课体";

        public Guid 插件识别码 => new Guid("ABDFB6DD-90B9-4B4A-A8A4-4D60996C948D");

        private record 课体题目(string 课体名) : I课体题目 { }
        private record 课体内容(bool 属此课体) : I课体内容 { }

        private readonly static Dictionary<string, Func<I年月日时, bool>> 测试课体表 = new() {
            { "测试一", (年月日时) => true },
            { "测试昼占", (年月日时) => 年月日时.昼占 },
            { "测试夜占", (年月日时) => !年月日时.昼占 },
        };

        public IEnumerable<I课体题目> 支持的课体
        {
            get
            {
                return 测试课体表.Keys.Select(k => new 课体题目(k));
            }
        }

        public I课体内容 识别课体(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表, string 课体题目)
        {
            return new 课体内容(测试课体表[课体题目](年月日时));
        }
    }
}
