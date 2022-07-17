using SixRens.Api.实体;

namespace SixRens.DefaultPlugins.参考
{
    internal sealed class 参考测试实现
    {
        public 参考测试实现() { }

        private sealed record 参考(
            string 题目,
            string 内容) : I占断参考
        { }

        public IEnumerable<I占断参考> 取参考()
        {
            yield return new 参考("参考测试一", "这是一个测试参考");
            yield return new 参考("参考测试二", "这是第二个测试参考");
        }
    }
}
