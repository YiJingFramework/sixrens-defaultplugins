using com.nlf.calendar;
using SixRens.Api;
using SixRens.Core;
using SixRens.Core.实体;
using SixRens.Core.工具.年月日时;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.DefaultPlugins.年命;
using YiJingFramework.StemsAndBranches;

namespace 三传生成检测
{
    internal class Program
    {
        internal static void Main()
        {
            var 子 = new EarthlyBranch(1);

            using StreamReader sr = new StreamReader($"./三传表.txt");
            using StreamWriter sw = new StreamWriter($"./有误三传.txt", false);
            Lunar lunar = Solar.fromBaZi("甲子", "丙子", "甲子", "甲子").First().getLunar();
            for (int i = 0; i < 60; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    I年月日时信息 年月日时 = new 真实年月日时(lunar).修改信息(new EarthlyBranch(j));
                    var 式 = new 壬式(年月日时,
                        null,
                        Array.Empty<本命信息>(),
                        new 地盘默认(),
                        new 天盘月将加时(),
                        new 四课默认(),
                        // new 三传涉害深浅打表式(),
                        new 三传涉害深浅计算式(),
                        // new 三传插件(),
                        new 天将甲戊庚牛羊壬癸蛇兔藏(),
                        new 年命默认(),
                        Array.Empty<I神煞插件>(),
                        Array.Empty<I课体插件>(),
                        Array.Empty<I参考插件>());

                    var str1 = $"{年月日时.日干:C}{年月日时.日支:C} {式.取所乘神(子):C}加子";
                    var str2 = $"{式.三传.初传:C}{式.三传.中传:C}{式.三传.末传:C}";
                    var str3 = "";
                    if (str1 != sr.ReadLine()
                        | str2 != sr.ReadLine()
                        | str3 != sr.ReadLine())
                    {
                        Console.WriteLine(str1);
                        sw.WriteLine(str1);
                        sw.WriteLine(str2);
                        sw.WriteLine();
                    }
                }
                lunar = lunar.next(1);
            }

            sr.Dispose();
            sw.Dispose();
        }
    }
}