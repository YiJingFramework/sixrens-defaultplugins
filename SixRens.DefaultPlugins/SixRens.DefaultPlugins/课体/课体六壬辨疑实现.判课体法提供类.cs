using SixRens.Api;
using SixRens.Api.实体.壬式;
using SixRens.Api.工具;
using YiJingFramework.FiveElements;

namespace SixRens.DefaultPlugins.课体
{
    internal sealed partial class 课体六壬辨疑实现 : I课体插件.I课体内容提供器
    {
        private sealed record 壬式信息(I四课 四课);
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
        }
    }
}
