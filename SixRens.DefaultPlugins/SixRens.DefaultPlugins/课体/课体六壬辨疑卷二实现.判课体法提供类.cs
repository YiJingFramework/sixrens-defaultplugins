using SixRens.Api;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using SixRens.Tools;
using SixRens.Tools.十二长生扩展;
using SixRens.Tools.干支关系扩展;
using SixRens.Tools.旺相扩展;
using YiJingFramework.FiveElements;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.课体
{
    internal sealed partial class 课体六壬辨疑卷二实现 : I课体插件.I课体内容提供器
    {
        private sealed record 壬式信息(
            I起课信息 起课信息,
            I天地盘 天地盘,
            I四课 四课,
            I三传 三传,
            I天将盘 天将盘,
            IReadOnlyList<I神煞> 神煞列表)
        { }

        private delegate bool 判课体法(壬式信息 式, 缓存 存);

        [AttributeUsage(AttributeTargets.Method)]
        private sealed class 判课体法Attribute : Attribute { }
        private static class 判课体法提供类
        {
            private static IReadOnlyList<四课之一> 取四课(壬式信息 式, 缓存 存)
            {
                if (存.四课 is null)
                {
                    存.四课 = new 四课之一[4] {
                        new 四课之一(式.四课.日, 式.四课.日阳),
                        new 四课之一(式.四课.日阳, 式.四课.日阴),
                        new 四课之一(式.四课.辰, 式.四课.辰阳),
                        new 四课之一(式.四课.辰阳, 式.四课.辰阴),
                    };
                }
                return 存.四课;
            }
            private static IReadOnlyList<四课之一> 取不重复四课(壬式信息 式, 缓存 存)
            {
                if (存.不重复四课 is null)
                {
                    存.不重复四课 = 取四课(式, 存).DistinctBy(课 => 课.上).ToArray();
                }
                return 存.不重复四课;
            }
            private static (IReadOnlyList<四课之一> 课, bool 为贼) 取克课(壬式信息 式, 缓存 存)
            {
                if (!存.克课.HasValue)
                {
                    var 四课 = 取四课(式, 存);
                    var 下贼上课 = new List<四课之一>(4);
                    var 上克下课 = new List<四课之一>(4);
                    foreach (var 课 in 四课)
                    {
                        switch (课.下五行.GetRelationship(课.上五行))
                        {
                            case FiveElementsRelationship.OvercameByMe:
                                下贼上课.Add(课);
                                break;
                            case FiveElementsRelationship.OvercomingMe:
                                上克下课.Add(课);
                                break;
                        }
                    }
                    var 有贼否 = 下贼上课.Count is not 0;
                    存.克课 = (有贼否 ? 下贼上课 : 上克下课, 有贼否);
                }
                return 存.克课.Value;
            }
            private static IReadOnlyList<四课之一> 取遥克课(壬式信息 式, 缓存 存)
            {
                if (存.遥克课 is null)
                {
                    var 四课 = 取四课(式, 存);
                    var 干五行 = 四课[0].下五行;
                    存.遥克课 = 四课
                        .Where(课 => 干五行.GetRelationship(课.上五行) is
                        FiveElementsRelationship.OvercameByMe or FiveElementsRelationship.OvercomingMe)
                        .ToArray();
                }
                return 存.遥克课;
            }
            private static (bool 遵循顺逆, bool 为顺) 取天将顺逆(壬式信息 式, 缓存 存)
            {
                if (!存.天将顺逆.HasValue)
                {
                    IEnumerable<int> 顺行预期(int 起始)
                    {
                        for (int 序号 = 起始 + 12; 序号 < 起始 + 24; 序号++)
                        {
                            yield return 序号 % 12;
                        }
                    }
                    IEnumerable<int> 逆行预期(int 起始)
                    {
                        for (int 序号 = 起始 + 12; 序号 > 起始; 序号--)
                        {
                            yield return 序号 % 12;
                        }
                    }
                    IEnumerable<int> 取实际()
                    {
                        for (int 序号 = 1; 序号 <= 12; 序号++)
                        {
                            yield return (int)式.天将盘.取乘将(new(序号));
                        }
                    }

                    var 实际 = 取实际();
                    var 起 = 实际.First();
                    if(实际.SequenceEqual(顺行预期(起)))
                        存.天将顺逆 = (true, true);
                    else if (实际.SequenceEqual(逆行预期(起)))
                        存.天将顺逆 = (true, false);
                    else
                        存.天将顺逆 = (false, true);
                }
                return 存.天将顺逆.Value;
            }
            private static IEnumerable<EarthlyBranch> 迭代三传(壬式信息 式, 缓存 存)
            {
                yield return 式.三传.初传;
                yield return 式.三传.中传;
                yield return 式.三传.末传;
            }
            private static IEnumerable<EarthlyBranch> 迭代四课(壬式信息 式, 缓存 存, bool 含日 = true)
            {
                if (含日)
                    yield return 式.四课.日.寄宫();
                yield return 式.四课.日阳;
                yield return 式.四课.日阴;
                yield return 式.四课.辰;
                yield return 式.四课.辰阳;
                yield return 式.四课.辰阴;
            }
            #region 卷二
            [判课体法]
            public static bool 元首(壬式信息 式, 缓存 存)
            {
                var (课, 为贼) = 取克课(式, 存);
                if (为贼)
                    return false;
                return 课.DistinctBy(课 => 课.上).Count() is 1;
            }

            [判课体法]
            public static bool 重审(壬式信息 式, 缓存 存)
            {
                var (课, 为贼) = 取克课(式, 存);
                if (!为贼)
                    return false;
                return 课.DistinctBy(课 => 课.上).Count() is 1;
            }

            [判课体法]
            public static bool 知一(壬式信息 式, 缓存 存)
            {
                var (课, _) = 取克课(式, 存);
                var 去重课 = 课.DistinctBy(课 => 课.上).ToArray();
                if (去重课.Length is not 2)
                    return false;
                return 去重课[0].上阴阳 != 去重课[1].上阴阳;
            }

            [判课体法]
            public static bool 涉害(壬式信息 式, 缓存 存)
            {
                var (课, _) = 取克课(式, 存);
                var 去重课 = 课.DistinctBy(课 => 课.上).ToArray();
                if (去重课.Length is not 2)
                    return false;
                return 去重课[0].上阴阳 == 去重课[1].上阴阳;
            }

            [判课体法]
            public static bool 遥克(壬式信息 式, 缓存 存)
            {
                var (克课, _) = 取克课(式, 存);
                if (克课.Count is not 0)
                    return false;
                return 取遥克课(式, 存).Count is not 0;
            }

            [判课体法]
            public static bool 昂星(壬式信息 式, 缓存 存)
            {
                var (克课, _) = 取克课(式, 存);
                if (克课.Count is not 0)
                    return false;

                if (取遥克课(式, 存).Count is not 0)
                    return false;

                var 四课 = 取不重复四课(式, 存);
                return 四课.Count is 4;
            }

            [判课体法]
            public static bool 别责(壬式信息 式, 缓存 存)
            {
                var (克课, _) = 取克课(式, 存);
                if (克课.Count is not 0)
                    return false;

                if (取遥克课(式, 存).Count is not 0)
                    return false;

                var 四课 = 取不重复四课(式, 存);
                return 四课.Count is 3;
            }

            [判课体法]
            public static bool 八专(壬式信息 式, 缓存 存)
            {
                var (克课, _) = 取克课(式, 存);
                if (克课.Count is not 0)
                    return false;

                if (取遥克课(式, 存).Count is not 0)
                    return false;

#warning 伏吟八专可兼否？
                if (伏吟(式, 存))
                    return false;

                var 四课 = 取四课(式, 存);
                return 四课[0].支下或干下之寄宫 == 四课[2].支下或干下之寄宫;
            }

            [判课体法]
            public static bool 伏吟(壬式信息 式, 缓存 存)
            {
                var 四课 = 取四课(式, 存);
                return 四课[0].上 == 四课[0].支下或干下之寄宫;
            }

            [判课体法]
            public static bool 反吟(壬式信息 式, 缓存 存)
            {
                var 四课 = 取四课(式, 存);
                return 四课[0].上 == 四课[0].支下或干下之寄宫.取冲();
            }
            #endregion

            /*
            #region 卷三
            [判课体法]
            public static bool 三光(壬式信息 式, 缓存 存)
            {
#warning 曰吉神在中，然怎得判断天将如何？
                var 月 = 式.起课信息.年月日时.月支;
                return
                    式.起课信息.年月日时.日干.取旺相状态(月) is 旺相状态.旺 or 旺相状态.相 &&
                    式.起课信息.年月日时.日支.取旺相状态(月) is 旺相状态.旺 or 旺相状态.相 &&
                    式.三传.初传.取旺相状态(月) is 旺相状态.旺 or 旺相状态.相;
            }

            [判课体法]
            public static bool 三阳(壬式信息 式, 缓存 存)
            {
                var (符合顺逆, 顺) = 取天将顺逆(式, 存);
                if (!符合顺逆)
                    return false;
                if (!顺)
                    return false;
#warning 何谓有气？何谓居前？
                var 月 = 式.起课信息.年月日时.月支;
                bool 有气居贵神前(EarthlyBranch 支)
                {
                    return 支.取旺相状态(月) is 旺相状态.旺 or 旺相状态.相 &&
                        ((int)式.天将盘.取乘将(支)) is > 0 and <= 6;
                }
                return
                    (有气居贵神前(式.起课信息.年月日时.日干.寄宫()) ||
                    有气居贵神前(式.起课信息.年月日时.日支)) &&
                    式.三传.初传.取旺相状态(月) is 旺相状态.旺 or 旺相状态.相;
            }

            [判课体法]
            public static bool 三奇(壬式信息 式, 缓存 存)
            {
                EarthlyBranch 旬奇 = new(
                    式.起课信息.年月日时.旬所在.旬首支.Index switch {
                        1 or 11 => 2,
                        9 or 7 => 1,
                        _ => 12 // 5 or 3
                    });
                if (迭代三传(式, 存).Contains(旬奇))
                    return true;
                return false;
            }

            [判课体法]
            public static bool 六仪(壬式信息 式, 缓存 存)
            {
                return 迭代三传(式, 存).Contains(式.起课信息.年月日时.旬所在.旬首支);
            }

            [判课体法]
            public static bool 时泰(壬式信息 式, 缓存 存)
            {
                if (!迭代三传(式, 存).Contains(式.起课信息.年月日时.年支))
                    return false;
                if (!迭代三传(式, 存).Contains(式.起课信息.年月日时.月支))
                    return false;
#warning 还未写完
                return false;
            }
            #endregion
            */
        }
    }
}
