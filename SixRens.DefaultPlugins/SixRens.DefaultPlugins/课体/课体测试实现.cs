using SixRens.Api.实体;

namespace SixRens.DefaultPlugins.课体
{
    internal sealed class 课体测试实现
    {
        private sealed record 壬式(I年月日时 年月日时);

        private readonly 壬式 式;

        public 课体测试实现(I年月日时 年月日时)
        {
            式 = new(年月日时);
        }

        private sealed record 课体(
            string 课体名) : I课体
        { }

        public IEnumerable<I课体> 取课体()
        {
            yield return new 课体("测试一");
            if (式.年月日时.昼占)
            {
                yield return new 课体("测试昼占");
            }
            else
            {
                yield return new 课体("测试夜占");
            }
        }
    }
}
