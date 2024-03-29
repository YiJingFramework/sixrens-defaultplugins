﻿using com.nlf.calendar;
using SixRens.Api;
using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理;
using SixRens.Core.插件管理.预设管理;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.天将;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;
using static SixRens.Core.插件管理.预设管理.经过解析的预设;

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
            new 三传涉害深浅计算式(),
            new 天将甲戊庚牛羊壬癸蛇兔藏(),
            Array.Empty<实体题目和所属插件<I神煞插件>>(),
            Array.Empty<实体题目和所属插件<I课体插件>>(),
            Array.Empty<I参考插件>());

        var 式 = new 壬式(new(年月日时,
            null, Array.Empty<年命>()),
            预设);

        sw.Write("                ");
        sw.Write(GenerateKey(式.四课.日, 式.四课.辰, 式.天地盘.取乘神(子)));
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
