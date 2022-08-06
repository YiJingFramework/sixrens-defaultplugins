using SixRens.Api;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.课体
{
    internal sealed partial class 课体六壬辨疑实现 : I课体插件.I课体内容提供器
    {
        private static readonly Dictionary<string, 判课体法> 判课体法列表;
        public static IEnumerable<string> 支持的课体 => 判课体法列表.Keys;
        static 课体六壬辨疑实现()
        {
            判课体法列表 = typeof(判课体法提供类)
                .GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(判课体法Attribute), false).Length is not 0)
                .ToDictionary((m) => m.Name, (m) => m.CreateDelegate<判课体法>());
        }

        private readonly 壬式信息 壬式;
        private readonly 缓存 存;
        internal 课体六壬辨疑实现(I起课信息 起课信息, I天地盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, IReadOnlyList<I神煞> 神煞列表)
        {
            this.壬式 = new(起课信息, 天盘, 四课, 三传, 天将盘, 神煞列表);
            this.存 = new();
        }

        public bool 属此课体(string 课体名)
        {
            return 判课体法列表[课体名](壬式, 存);
        }
    }
}
