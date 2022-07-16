using com.nlf.calendar;
using SixRens;
using SixRens.Api;
using SixRens.Api.实体;
using SixRens.DefaultPlugins.三传;
using SixRens.DefaultPlugins.四课;
using SixRens.DefaultPlugins.地盘;
using SixRens.DefaultPlugins.天将;
using SixRens.DefaultPlugins.天盘;
using SixRens.DefaultPlugins.年命;
using SixRens.实体;
using SixRens.扩展;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;
using 三传打表式三传表生成器;
using 三传打表式三传表生成器.三传取法;

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
        var 式 = new 壬式(年月日时,
            null,
            Array.Empty<本命信息>(),
            new 地盘默认(),
            new 天盘月将加时(),
            new 四课默认(),
            new 三传插件(),
            new 天将甲戊庚牛羊壬癸蛇兔藏(),
            new 年命默认(),
            Array.Empty<I神煞插件>(),
            Array.Empty<I课体插件>());

        sw.Write("                ");
        sw.Write(GenerateKey(式.四课.日, 式.四课.辰, 式.取所乘神(子)));
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