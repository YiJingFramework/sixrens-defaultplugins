using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.工具;
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
        private const string 空格 = "　";
        private static void 试打印神煞(壬式 壬式)
        {
            foreach (var 神煞 in 壬式.神煞)
            {
                var 神 = string.Join(空格, 神煞.所在神.Select(神 => 神.ToString("C")));
                Console.WriteLine($"{神煞.神煞名}：{神}");
            }
            Console.WriteLine();
        }
        private static void 打印课体(壬式 壬式)
        {
            Console.WriteLine(string.Join(空格, 壬式.课体.Select(体 => 体.课体名)));
            Console.WriteLine();
        }
        private static void 打印参考(壬式 壬式)
        {
            foreach (var 参考 in 壬式.占断参考.Where(参考 => 参考.内容 is not null))
            {
                Console.Write(参考.题目);
                Console.WriteLine("：");
                Console.WriteLine(参考.内容);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Main()
        {
            var 插件包 = 加载插件包();

            var 预设 = new 经过解析的预设(
                插件包.地盘插件[0],
                插件包.天盘插件[0],
                插件包.四课插件[0],
                插件包.三传插件[0],
                插件包.天将插件[0],
                插件包.年命插件[0],
                插件包.神煞插件.SelectMany(c => c.支持的神煞,
                    (c, s) => new 实体题目和所属插件<I神煞插件>(c, s.神煞名)),
                插件包.课体插件.SelectMany(c => c.支持的课体,
                    (c, s) => new 实体题目和所属插件<I课体插件>(c, s.课体名)),
                插件包.参考插件);

            测试(预设);
            Console.WriteLine("============");

            预设 = 预设 = new 经过解析的预设(
                插件包.地盘插件[0],
                插件包.天盘插件[0],
                插件包.四课插件[0],
                插件包.三传插件[1],
                插件包.天将插件[0],
                插件包.年命插件[0],
                插件包.神煞插件.SelectMany(c => c.支持的神煞,
                    (c, s) => new 实体题目和所属插件<I神煞插件>(c, s.神煞名)),
                插件包.课体插件.SelectMany(c => c.支持的课体,
                    (c, s) => new 实体题目和所属插件<I课体插件>(c, s.课体名)),
                插件包.参考插件);

            测试(预设);
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

            壬式 壬式 = new 壬式(年月日时,
                new 本命信息(YinYang.Yang, new EarthlyBranch(7)),
                new[] { new 本命信息(YinYang.Yin, new EarthlyBranch(8)) },
                预设);
            Console.WriteLine(壬式.创建占例().可读文本化());
        }

        private static void 打印年月日时(壬式 壬式)
        {
            var 年月日时 = 壬式.年月日时;
            Console.WriteLine($"{年月日时.年干:C}{年月日时.年支:C}年{空格}" +
                $"{年月日时.月干:C}{年月日时.月支:C}月{空格}" +
                $"{年月日时.日干:C}{年月日时.日支:C}日{空格}" +
                $"{年月日时.时干:C}{年月日时.时支:C}时");
            Console.WriteLine($"{年月日时.旬所在.旬首干:C}{年月日时.旬所在.旬首支:C}旬{空格}" +
                $"{年月日时.旬所在.空亡一:C}{年月日时.旬所在.空亡二:C}空{空格}" +
                $"{年月日时.月将:C}将");
            Console.WriteLine();
        }
        private static void 打印年命(壬式 壬式)
        {
            string 转字符串(I年命 年命)
            {
                return $"{(年命.性别.IsYang ? '男' : '女')}{年命.本命:C}命{年命.行年:C}年";
            }
            var 课主 = 壬式.课主年命;
            if (课主 is not null)
                Console.WriteLine($"课主{空格}{转字符串(课主)}");
            if (壬式.对象年命.Count != 0)
            {
                var 年命字符串表 = 壬式.对象年命.Select(年命 => 转字符串(年命));
                Console.WriteLine($"对象{空格}{string.Join(空格, 年命字符串表)}");
            }
            Console.WriteLine();
        }
        private static void 打印三传(壬式 壬式)
        {
            string 生成字符串(EarthlyBranch 支)
            {
                var 旬 = 壬式.年月日时.旬所在;
                var 落空 = 旬.获取对应天干(壬式.取上神(支)).HasValue ? $"{空格}{空格}" : "落空";
                var 六亲 = 壬式.年月日时.日干.判断六亲(支);
                var 遁干 = 旬.获取对应天干(支)?.ToString("C") ?? 空格;
                var 天将 = 壬式.取乘将(支).简称();
                return $"{落空}{空格}{六亲}{空格}{遁干}{支:C}{天将}";
            }

            var 初 = 壬式.三传.初传;
            var 中 = 壬式.三传.中传;
            var 末 = 壬式.三传.末传;
            Console.WriteLine(生成字符串(初));
            Console.WriteLine(生成字符串(中));
            Console.WriteLine(生成字符串(末));
            Console.WriteLine();
        }
        private static void 打印四课(壬式 壬式)
        {
            Console.WriteLine($"{壬式.取乘将(壬式.四课.辰阴).简称()}" +
                $"{壬式.取乘将(壬式.四课.辰阳).简称()}" +
                $"{壬式.取乘将(壬式.四课.日阴).简称()}" +
                $"{壬式.取乘将(壬式.四课.日阳).简称()}");
            Console.WriteLine($"{壬式.四课.辰阴:C}{壬式.四课.辰阳:C}" +
                $"{壬式.四课.日阴:C}{壬式.四课.日阳:C}");
            Console.WriteLine($"{壬式.四课.辰阳:C}{壬式.四课.辰:C}" +
                $"{壬式.四课.日阳:C}{壬式.四课.日:C}");
            Console.WriteLine();
        }
        private static void 打印天盘(壬式 壬式)
        {
            var branches = Enumerable.Range(1, 12)
                .ToDictionary(item => item, item => new EarthlyBranch(item));

            Console.WriteLine($"{壬式.取上神(branches[6]):C}{壬式.取上神(branches[7]):C}" +
                $"{壬式.取上神(branches[8]):C}{壬式.取上神(branches[9]):C}");
            Console.WriteLine($"{壬式.取上神(branches[5]):C}{空格}{空格}" +
                $"{壬式.取上神(branches[10]):C}");
            Console.WriteLine($"{壬式.取上神(branches[4]):C}{空格}{空格}" +
                $"{壬式.取上神(branches[11]):C}");
            Console.WriteLine($"{壬式.取上神(branches[3]):C}{壬式.取上神(branches[2]):C}" +
                $"{壬式.取上神(branches[1]):C}{壬式.取上神(branches[12]):C}");
            Console.WriteLine();
        }
    }
}