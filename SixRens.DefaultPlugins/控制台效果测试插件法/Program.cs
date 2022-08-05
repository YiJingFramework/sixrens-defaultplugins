using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Core.名称转换;
using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理;
using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;
using System.Diagnostics;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;
using static SixRens.Core.插件管理.预设管理.经过解析的预设;

namespace 控制台效果测试插件法
{
    public class Program
    {
        public static void Main()
        {
            var 插件包 = 加载插件包();

            var 预设 = new 经过解析的预设(
                插件包.三传插件[0],
                插件包.天将插件[0],
                插件包.神煞插件.SelectMany(c => c.支持神煞的名称,
                    (c, s) => new 实体题目和所属插件<I神煞插件>(c, s)),
                插件包.课体插件.SelectMany(c => c.支持的课体,
                    (c, s) => new 实体题目和所属插件<I课体插件>(c, s)),
                插件包.参考插件);

            测试(预设);
            Console.WriteLine("============");

            /*
            预设 = 预设 = new 经过解析的预设(
                插件包.三传插件[1],
                插件包.天将插件[0],
                插件包.神煞插件.SelectMany(c => c.支持神煞的名称,
                    (c, s) => new 实体题目和所属插件<I神煞插件>(c, s)),
                插件包.课体插件.SelectMany(c => c.支持的课体,
                    (c, s) => new 实体题目和所属插件<I课体插件>(c, s)),
                插件包.参考插件);

            测试(预设);
            */

            _ = Console.ReadLine();
        }

        private static 插件包 加载插件包()
        {
            Console.Write("路径：");
            var 包路径 = Console.ReadLine();
            Debug.Assert(包路径 is not null);
            using var 插件包流 = File.OpenRead(包路径);
            var 插件包 = new 插件包管理器(new 存储器()).从外部加载插件包(插件包流);
            Debug.Assert(插件包 is not null);
            return 插件包;
        }

        public static void 测试(经过解析的预设 预设)
        {
            // var time = new DateTime(2022, 2, 24, 22, 00, 0);
            var time = DateTime.Now;
            I年月日时信息 年月日时 = new 真实年月日时(time);

            壬式 壬式 = new 壬式(new(年月日时,
                new 年命(性别.男, new EarthlyBranch(7), 年月日时),
                new[] { new 年命(性别.女, new EarthlyBranch(8), 年月日时) }),
                预设);
            Console.WriteLine(壬式.创建占例().可读文本化());
        }
    }
}