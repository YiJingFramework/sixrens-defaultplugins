using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.课体
{
    public sealed class 课体测试实现 : I课体插件.I课体内容提供器
    {
        private readonly I年月日时 年月日时;

        internal 课体测试实现(I年月日时 年月日时)
        {
            this.年月日时 = 年月日时;
        }

        private readonly static Dictionary<string, Func<I年月日时, bool>> 测试课体表 = new() {
            { "测试一", (年月日时) => true },
            { "测试昼占", (年月日时) => 年月日时.昼占 },
            { "测试夜占", (年月日时) => !年月日时.昼占 },
        };

        public bool 属此课体(string 课体名)
        {
            return 测试课体表[课体名](年月日时);
        }
    }
}
