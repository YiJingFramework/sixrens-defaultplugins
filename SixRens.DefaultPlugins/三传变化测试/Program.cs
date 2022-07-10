﻿using com.nlf.calendar;
using SixRens;
using SixRens.Api;
using SixRens.Api.实体;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.扩展;
using YiJingFramework.StemsAndBranches;
using 三传变化测试;

namespace Test
{
    internal class Program
    {
        internal static void Main()
        {
            // 甲子 子 癸酉 子 辰 （113）
            // 甲子 子 己卯 子 辰 （185）


            var 子 = new EarthlyBranch(1);

            Lunar lunar = Solar.fromBaZi("甲子", "丙子", "甲子", "甲子").First().getLunar();
            int count = 1;
            for (int i = 0; i < 60; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    I年月日时 年月日时 = new 虚假年月日时(lunar, new EarthlyBranch(j));
                    var 式 = new 壬式(年月日时,
                        new 地盘直用基础盘(),
                        new 天盘月将加子(),
                        new 四课日上上(),
                        new 三传依深浅(),
                        new 天将甲戊庚牛羊壬癸蛇兔藏(),
                        Array.Empty<I神煞插件>());

                    var fileName = $"SixRens结果_{count.ToString().PadLeft(3, '0')}{年月日时.日干:C}{年月日时.日支:C}{年月日时.月将:C}.txt";
                    count++;
                    using StreamWriter sw = new StreamWriter($"./SixRens结果单个/{fileName}", false);

                    sw.WriteLine($"{年月日时.年干:C}{年月日时.年支:C} {年月日时.月支:C} {年月日时.日干:C}{年月日时.日支:C} {年月日时.时支:C} {年月日时.月将:C}");
                    sw.WriteLine($"子乘：{式.取所乘神(子):C}");
                    sw.WriteLine($"三传：{式.三传.初传:C}{式.三传.中传:C}{式.三传.末传:C}");
                    sw.WriteLine();

                    sw.Dispose();
                    if (File.ReadAllText($"./SixRens结果单个/{fileName}") != File.ReadAllText($"./曾经的结果单个/{fileName}"))
                        Console.WriteLine(fileName);
                    else
                        File.Delete($"./SixRens结果单个/{fileName}");
                }
                lunar = lunar.next(1);
            }

        }
    }
}