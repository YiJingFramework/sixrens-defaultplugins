﻿using com.nlf.calendar;
using SixRens.Api;
using SixRens.Core;
using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.DefaultPlugins.年命;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;
using 三传取法;
using static SixRens.Core.插件管理.经过解析的预设;

int GenerateKey(HeavenlyStem 日, EarthlyBranch 辰, EarthlyBranch 子所乘)
{
    Debug.Assert(日.Index * 10000L + 辰.Index * 100L + 子所乘.Index < int.MaxValue);
    return 日.Index * 10000 + 辰.Index * 100 + 子所乘.Index;
}

using StreamWriter sw = new StreamWriter("output.txt", false);

sw.WriteLine("        private static (int 初, int 中, int 末) 获取三传(int 键)");
sw.WriteLine("        {");
sw.WriteLine("            return 键 switch");
sw.WriteLine("            {");

var 子 = new EarthlyBranch(1);

Lunar lunar = Solar.fromBaZi("甲子", "丙子", "甲子", "甲子").First().getLunar();
for (int i = 0; i < 60; i++)
{
    for (int j = 1; j <= 12; j++)
    {
        I年月日时信息 年月日时 = new 真实年月日时(lunar).修改信息(new EarthlyBranch(j));
        var 预设 = new 经过解析的预设(
            new 地盘默认(),
            new 天盘月将加时(),
            new 四课默认(),
            new 三传插件(),
            new 天将甲戊庚牛羊壬癸蛇兔藏(),
            new 年命默认(),
            Array.Empty<实体题目表和所属插件<I神煞插件>>(),
            Array.Empty<实体题目表和所属插件<I课体插件>>(),
            Array.Empty<I参考插件>());

        var 式 = new 壬式(年月日时,
            null, Array.Empty<本命信息>(),
            预设);

        sw.Write("                ");
        sw.Write(GenerateKey(式.四课.日, 式.四课.辰, 式.取上神(子)));
        sw.Write(" => (");
        sw.Write(式.三传.初传.Index);
        sw.Write(", ");
        sw.Write(式.三传.中传.Index);
        sw.Write(", ");
        sw.Write(式.三传.末传.Index);
        sw.WriteLine("),");
    }
    lunar = lunar.next(1);
}
sw.WriteLine("                _ => (0, 0, 0)");
sw.WriteLine("            };");
sw.WriteLine("        }");
