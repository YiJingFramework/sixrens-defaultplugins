using SixRens;
using SixRens.Api.实体;
using SixRens.Api.工具;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.DefaultPlugins.年命;
using SixRens.DefaultPlugins.神煞;
using SixRens.DefaultPlugins.课体;
using SixRens.实体;
using SixRens.扩展;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace 控制台效果测试
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
        public static void Main()
        {
            var time = new DateTime(2022, 2, 24, 22, 00, 0);
            I年月日时信息 年月日时 = new 真实年月日时(time);
            壬式 壬式 = new 壬式(年月日时,
                new 本命信息(YinYang.Yang, new EarthlyBranch(7)),
                new[] { new 本命信息(YinYang.Yin, new EarthlyBranch(8)) },
                new 地盘默认(),
                new 天盘月将加时(),
                new 四课默认(),
                new 三传涉害深浅(),
                new 天将甲戊庚牛羊壬癸蛇兔藏(),
                new 年命默认(),
                new[] { new 神煞测试() },
                new[] { new 课体测试() });
            打印年月日时(壬式);
            打印年命(壬式);
            打印三传(壬式);
            打印四课(壬式);
            打印天盘(壬式);
            打印课体(壬式);
            试打印神煞(壬式);
            _ = Console.ReadLine();
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
            if(课主 is not null)
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
                var 落空 = 旬.获取对应天干(壬式.取所临神(支)).HasValue ? $"{空格}{空格}" : "落空";
                var 六亲 = 壬式.年月日时.日干.判断六亲(支);
                var 遁干 = 旬.获取对应天干(支)?.ToString("C") ?? 空格;
                var 天将 = (天将简称)壬式.取所乘将(支);
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
            Console.WriteLine($"{(天将简称)壬式.取所乘将(壬式.四课.辰阴)}" +
                $"{(天将简称)壬式.取所乘将(壬式.四课.辰阳)}" +
                $"{(天将简称)壬式.取所乘将(壬式.四课.日阴)}" +
                $"{(天将简称)壬式.取所乘将(壬式.四课.日阳)}");
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

            Console.WriteLine($"{壬式.取所乘神(branches[6]):C}{壬式.取所乘神(branches[7]):C}" +
                $"{壬式.取所乘神(branches[8]):C}{壬式.取所乘神(branches[9]):C}");
            Console.WriteLine($"{壬式.取所乘神(branches[5]):C}{空格}{空格}" +
                $"{壬式.取所乘神(branches[10]):C}");
            Console.WriteLine($"{壬式.取所乘神(branches[4]):C}{空格}{空格}" +
                $"{壬式.取所乘神(branches[11]):C}");
            Console.WriteLine($"{壬式.取所乘神(branches[3]):C}{壬式.取所乘神(branches[2]):C}" +
                $"{壬式.取所乘神(branches[1]):C}{壬式.取所乘神(branches[12]):C}");
            Console.WriteLine();
        }
    }
}
