using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.课体
{
    internal sealed partial class 课体六壬辨疑实现 : I课体插件.I课体内容提供器
    {
        private sealed class 缓存
        {
            public IReadOnlyList<四课之一>? 四课 { get; set; }
            public (IReadOnlyList<四课之一> 课, bool 为贼)? 克课 { get; set; }
            public IReadOnlyList<四课之一>? 遥克课 { get; set; }
            public IReadOnlyList<四课之一>? 不重复四课 { get; set; }
        }
    }
}
