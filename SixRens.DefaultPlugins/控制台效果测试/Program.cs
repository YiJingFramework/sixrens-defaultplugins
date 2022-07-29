using SixRens.Api;
using SixRens.Core.插件管理;
using SixRens.Core.插件管理.预设管理;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.参考;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.DefaultPlugins.年命;
using SixRens.DefaultPlugins.神煞;
using SixRens.DefaultPlugins.课体;
using static SixRens.Core.插件管理.预设管理.经过解析的预设;

namespace 控制台效果测试
{
    public class Program
    {
        public static void Main()
        {
            var 预设 = new 经过解析的预设(
                new 地盘默认(),
                new 天盘月将加时(),
                new 四课默认(),
                new 三传涉害深浅打表式(),
                // new 三传涉害深浅计算式(),
                new 天将甲戊庚牛羊壬癸蛇兔藏(),
                new 年命默认(),
                new[] { new 神煞六壬辨疑神煞纪要() }.SelectMany(c => c.支持的神煞,
                    (c, s) => new 实体题目和所属插件<I神煞插件>(c, s.神煞名)),
                new[] { new 课体测试() }.SelectMany(c => c.支持的课体,
                    (c, s) => new 实体题目和所属插件<I课体插件>(c, s.课体名)),
                new[] { new 参考测试() });
            控制台效果测试插件法.Program.测试(预设);
            _ = Console.ReadLine();
        }
    }
}
