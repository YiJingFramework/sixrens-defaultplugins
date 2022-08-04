using com.nlf.calendar;
using SixRens.Api;
using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理;
using SixRens.Core.插件管理.预设管理;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.天将;
using YiJingFramework.StemsAndBranches;
using 三传取法;
using static SixRens.Core.插件管理.预设管理.经过解析的预设;

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

                    var 预设 = new 经过解析的预设(
                        new 三传涉害深浅打表式(),
                        // new 三传涉害深浅计算式(),
                        // new 三传插件(),
                        new 天将甲戊庚牛羊壬癸蛇兔藏(),
                        Array.Empty<实体题目和所属插件<I神煞插件>>(),
                        Array.Empty<实体题目和所属插件<I课体插件>>(),
                        Array.Empty<I参考插件>());

                    var 式 = new 壬式(new(年月日时,
                        null, Array.Empty<年命>()),
                        预设);

                    var str1 = $"{年月日时.日干:C}{年月日时.日支:C} {式.天地盘.取乘神(子):C}加子";
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