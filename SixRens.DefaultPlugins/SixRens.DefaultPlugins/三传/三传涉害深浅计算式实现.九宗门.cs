using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.工具;
using System.Diagnostics;
using YiJingFramework.Core;
using YiJingFramework.FiveElements;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.三传
{
    internal partial class 三传涉害深浅计算式实现
    {
        internal void 重新计算()
        {
            var 四课 = new 四课之一[4]
            {
                new 四课之一(this.四课.日, this.四课.日阳),
                new 四课之一(this.四课.日阳, this.四课.日阴),
                new 四课之一(this.四课.辰, this.四课.辰阳),
                new 四课之一(this.四课.辰阳, this.四课.辰阴)
            };

            List<四课之一> 不重复原序课 = new List<四课之一>(4);
            HashSet<EarthlyBranch> 上 = new HashSet<EarthlyBranch>(4);
            foreach (var 课 in 四课)
            {
                if (上.Add(课.上))
                    不重复原序课.Add(课);
            }

            Debug.Assert(不重复原序课.Count is 1 or 2 or 3 or 4);
            switch (不重复原序课.Count)
            {
                case 4:
                    this.四课皆备(四课);
                    break;
                case 3:
                    this.还有三课(四课, 不重复原序课);
                    break;
                case 2:
                    this.止有两课(四课);
                    break;
                default:
                    this.唯有一课(四课);
                    break;
            }
        }

        #region 以课全或不全分类起传
        private void 四课皆备(四课之一[] 四课)
        {
            Debug.Assert(四课.Length is 4);

            {
                // 贼克系列
                var 贼克者 = 取贼或无贼取克(四课, out bool 有贼否);
                if (贼克者.Count is 1)
                {
                    this.以初传其乘再乘设三传(贼克者.Single().上);
                    return;
                }
                if (贼克者.Count is not 0)
                {
                    Debug.Assert(贼克者.Count is 2 or 3 or 4);

                    IReadOnlyList<四课之一> 比日者 =
                        取上比者(贼克者, 日阴阳: 四课[1 - 1].下阴阳).ToList();

                    if (比日者.Count is 1)
                    {
                        this.以初传其乘再乘设三传(比日者.Single().上);
                        return;
                    }

                    比日者 = 比日者.Count is 0 ? 贼克者 : 比日者;

                    var 涉害者 = 有贼否 ? 下贼上比较涉害程度取(比日者) : 上克下比较涉害程度取(比日者);
                    Debug.Assert(涉害者.Count is not 0);

                    if (涉害者.Count == 1)
                    {
                        this.以初传其乘再乘设三传(涉害者[0].上);
                        return;
                    }
                    var 孟仲者 = 按下孟仲取(涉害者);

                    if (孟仲者.Any())
                        this.以初传其乘再乘设三传(孟仲者.First().上);
                    else
                    {
                        if (四课[1 - 1].下阴阳.IsYang)
                            this.以初传其乘再乘设三传(四课[1 - 1].上);
                        else
                            this.以初传其乘再乘设三传(四课[3 - 1].上);
                    }
                    return;
                }
            }

            {
                // 遥克系列
                var 遥克者 = 取遥克(四课, 四课[1 - 1].下五行, out _);
                if (遥克者.Count is 1)
                {
                    this.以初传其乘再乘设三传(遥克者.Single().上);
                    return;
                }
                if (遥克者.Count is not 0)
                {
                    Debug.Assert(遥克者.Count is 2 or 3 or 4);

                    var 比日者 = 取上比者(遥克者, 日阴阳: 四课[1 - 1].下阴阳);

                    Debug.Assert(比日者.Count() is 1, "不确定！但是不知道其他情况应该怎么办。");
                    if (比日者.Count() is not 1)
                        throw new 起课失败异常("无法起出三传");

                    this.以初传其乘再乘设三传(比日者.Single().上);
                    return;
                }
            }

            if (四课[1 - 1].支下或干下之寄宫.取冲() == 四课[1 - 1].上)
            {
                this.反吟无克设三传(四课);
                return;
            }

            {
                // 昂星
                var 酉 = new EarthlyBranch(10);
                var 日阴阳 = 四课[1 - 1].下阴阳;
                if (日阴阳.IsYang)
                {
                    this.初传 = this.天地盘.取乘神(酉);
                    this.中传 = 四课[3 - 1].上;
                    this.末传 = 四课[1 - 1].上;
                    return;
                }

                this.初传 = this.天地盘.取临地(酉);
                this.中传 = 四课[1 - 1].上;
                this.末传 = 四课[3 - 1].上;
                return;
            }
        }
        private void 还有三课(四课之一[] 四课, IReadOnlyList<四课之一> 不重复三课)
        {
            Debug.Assert(四课.Length is 4);
            Debug.Assert(不重复三课.Count is 3);

            {
                // 贼克系列
                var 四课贼克者 = 取贼或无贼取克(四课, out bool 有贼否);

                if (四课贼克者.DistinctBy(课 => 课.上).Count() is 1)
                {
                    this.以初传其乘再乘设三传(四课贼克者[0].上);
                    return;
                }

                if (四课贼克者.Count is not 0)
                {
                    Debug.Assert(四课贼克者.Count is 2 or 3 or 4);

                    var 比日者 = 取上比者(四课贼克者, 日阴阳: 四课[1 - 1].下阴阳);

                    switch (比日者.Count())
                    {
                        case 0:
                            Debug.Fail("不确定，但好像确实如此。");
                            return;
                        case 1:
                            this.以初传其乘再乘设三传(比日者.Single().上);
                            return;
                        case 2 when 比日者.DistinctBy(课 => 课.上).Count() is 1:
                            this.以初传其乘再乘设三传(比日者.First().上);
                            return;
                    }

                    var 涉害者 = 有贼否 ? 下贼上比较涉害程度取(比日者) : 上克下比较涉害程度取(比日者);
                    Debug.Assert(涉害者.Count is not 0);

                    if (涉害者.DistinctBy(课 => 课.上).Count() == 1)
                    {
                        this.以初传其乘再乘设三传(涉害者[0].上);
                        return;
                    }
                    var 孟仲者 = 按下孟仲取(涉害者);

                    if (孟仲者.Any())
                        this.以初传其乘再乘设三传(孟仲者.First().上);
                    else
                    {
                        if (四课[1 - 1].下阴阳.IsYang)
                            this.以初传其乘再乘设三传(四课[1 - 1].上);
                        else
                            this.以初传其乘再乘设三传(四课[3 - 1].上);
                    }
                    return;
                }
            }

            {
                // 遥克系列
                var 遥克者 = 取遥克(不重复三课, 四课[1 - 1].下五行, out _);
                if (遥克者.Count is 1)
                {
                    this.以初传其乘再乘设三传(遥克者.Single().上);
                    return;
                }
                if (遥克者.Count is not 0)
                {
                    Debug.Assert(遥克者.Count is 2 or 3);

                    var 比日者 = 取上比者(遥克者, 日阴阳: 四课[1 - 1].下阴阳);

                    Debug.Assert(比日者.Count() is 1, "不确定！但是不知道其他情况应该怎么办。");
                    if (比日者.Count() is not 1)
                        throw new 起课失败异常("无法起出三传");

                    this.以初传其乘再乘设三传(比日者.Single().上);
                    return;
                }
            }

            if (四课[1 - 1].支下或干下之寄宫.取冲() == 四课[1 - 1].上)
            {
                this.反吟无克设三传(四课);
                return;
            }

            {
                // 别责
                this.中传 = 四课[1 - 1].上;
                this.末传 = this.中传;

                var 日阴阳 = 四课[1 - 1].下阴阳;
                if (日阴阳.IsYang)
                {
                    var 日 = 四课[1 - 1].干下;
                    Debug.Assert(日.HasValue);

                    this.初传 = this.天地盘.取乘神(日.Value.取所合干().寄宫());
                    return;
                }

                var 辰 = 四课[3 - 1].支下或干下之寄宫;
                this.初传 = 辰.取所在三合局前一支();
                return;
            }
        }
        private void 止有两课(四课之一[] 四课)
        {
            Debug.Assert(四课.Length is 4);

            if (四课[1 - 1].支下或干下之寄宫 == 四课[3 - 1].支下或干下之寄宫)
            {
                // 八专
                {
                    // 贼克
                    var 贼者 = new List<int>(4);
                    var 克者 = new List<int>(4);
                    for (int i = 0; i < 3; i++)
                    {
                        switch (四课[i].下五行.GetRelationship(四课[i].上五行))
                        {
                            case FiveElementsRelationship.OvercameByMe:
                                贼者.Add(i);
                                break;
                            case FiveElementsRelationship.OvercomingMe:
                                克者.Add(i);
                                break;
                        }
                    }

                    Debug.Assert(贼者.Count is 0 or 1 or 2 or 3);

                    var 贼否 = 贼者.Count is not 0;
                    if (!贼否)
                        贼者 = 克者;

                    switch (贼者.Count)
                    {
                        case 0:
                            break;
                        case 1:
                            this.以初传其乘再乘设三传(四课[贼者.Single()].上);
                            return;
                        case 2:
                            if (贼者[0] == 0 && 贼者[1] == 2)
                            {
                                this.以初传其乘再乘设三传(四课[0].上);
                                return;
                            }
                            break;
                        case 3:
                            贼者.RemoveAt(2);
                            break;
                    }

                    var 贼克者 = 贼者.Select(item => 四课[item]);

                    if (贼者.Count is not 0)
                    {
                        Debug.Assert(贼克者.Count() is 2);

                        var 比日者 = 取上比者(贼克者, 日阴阳: 四课[1 - 1].下阴阳);

                        if (比日者.Count() is 1)
                        {
                            this.以初传其乘再乘设三传(比日者.Single().上);
                            return;
                        }

                        Debug.Assert(比日者.Count() is 2 or 3, "不确定！但是不知道为没有时应该怎么做。");

                        if (比日者.Count() is not 2 and not 3)
                            throw new 起课失败异常("无法起出三传");

                        var 涉害者 = 贼否 ? 下贼上比较涉害程度取(比日者) : 上克下比较涉害程度取(比日者);
                        Debug.Assert(涉害者.Count is not 0);

                        if (涉害者.Count == 1)
                        {
                            this.以初传其乘再乘设三传(涉害者[0].上);
                            return;
                        }
                        var 孟仲者 = 按下孟仲取(涉害者);

                        if (孟仲者.Any())
                            this.以初传其乘再乘设三传(孟仲者.First().上);
                        else
                        {
                            if (四课[1 - 1].下阴阳.IsYang)
                                this.以初传其乘再乘设三传(四课[1 - 1].上);
                            else
                                this.以初传其乘再乘设三传(四课[3 - 1].上);
                        }
                        return;
                    }
                }

                if (四课[1 - 1].支下或干下之寄宫.取冲() == 四课[1 - 1].上)
                {
                    this.反吟无克设三传(四课);
                    return;
                }

                var 日阳 = 四课[1 - 1].上;
                this.中传 = 日阳;
                this.末传 = this.中传;

                var 日阴阳 = 四课[1 - 1].下阴阳;
                if (日阴阳.IsYang)
                    this.初传 = 日阳.Next(2);
                else
                    this.初传 = 四课[4 - 1].上.Next(-2);
                return;
            }

            if (四课[1 - 1].支下或干下之寄宫.取冲() == 四课[1 - 1].上)
            {
                {
                    // 对两课贼克
                    var 贼克者 = 取贼或无贼取克(四课.Take(2), out bool 有贼否);
                    Debug.Assert(贼克者.Count is 0 or 1);

                    if (贼克者.Count is 1)
                    {
                        this.以初传其乘再乘设三传(贼克者.Single().上);
                        return;
                    }
                }

                this.反吟无克设三传(四课);
                return;
            }

            Debug.Assert(四课[1 - 1].支下或干下之寄宫 == 四课[1 - 1].上);
            this.伏吟(四课);
        }
        private void 唯有一课(四课之一[] 四课)
        {
            Debug.Assert(四课.Length is 4);
            Debug.Assert(四课[1 - 1].支下或干下之寄宫 == 四课[1 - 1].上);
            this.伏吟(四课);
            return;
        }
        #endregion

        #region 小方法
        private void 反吟无克设三传(四课之一[] 四课)
        {
            var 马 = 四课[3 - 1].支下或干下之寄宫.取所在三合局().长生支.Next(6);
            this.初传 = 马;
            this.中传 = 四课[3 - 1].上;
            this.末传 = 四课[1 - 1].上;
        }
        private void 伏吟(四课之一[] 四课)
        {
            Debug.Assert(四课.Length is 4);
            Debug.Assert(四课[1 - 1].支下或干下之寄宫 == 四课[1 - 1].上);

            var 日 = 四课[1 - 1].干下;
            Debug.Assert(日.HasValue);

            switch (日.Value.Index)
            {
                case 10: // 癸日 丑戌未
                    this.初传 = new EarthlyBranch(2);
                    this.中传 = new EarthlyBranch(11);
                    this.末传 = new EarthlyBranch(8);
                    return;
                case 2: // 乙日
                    this.初传 = new EarthlyBranch(5); // 辰发用
                    this.中传 = 四课[3 - 1].上;
                    var 刑神 = this.中传.取所刑();
                    this.末传 = 刑神 == this.中传 ? this.中传.取冲() : 刑神;
                    return;
                default:
                    if (四课[1 - 1].下阴阳.IsYang)
                    {
                        this.初传 = 四课[1 - 1].上;
                        刑神 = this.初传.取所刑();
                        this.中传 = 刑神 == this.初传 ? 四课[3 - 1].上 : 刑神;
                        刑神 = this.中传.取所刑();
                        // 末传 = 刑神 == 中传 ? 中传.取冲() : 刑神;
                        this.末传 = 刑神 == this.中传 || 刑神 == this.初传 ? this.中传.取冲() : 刑神;
                    }
                    else
                    {
                        this.初传 = 四课[3 - 1].上;
                        刑神 = this.初传.取所刑();
                        this.中传 = 刑神 == this.初传 ? 四课[1 - 1].上 : 刑神;
                        刑神 = this.中传.取所刑();
                        // 末传 = 刑神 == 中传 ? 中传.取冲() : 刑神;
                        this.末传 = 刑神 == this.中传 || 刑神 == this.初传 ? this.中传.取冲() : 刑神;
                    }
                    return;
            }

        }
        private void 以初传其乘再乘设三传(EarthlyBranch 初传)
        {
            this.初传 = 初传;
            this.中传 = this.天地盘.取乘神(初传);
            this.末传 = this.天地盘.取乘神(this.中传);
        }
        private static IReadOnlyList<四课之一> 取贼或无贼取克(
            IEnumerable<四课之一> 各课, out bool 有贼否)
        {
            var 下贼上课 = new List<四课之一>(4);
            var 上克下课 = new List<四课之一>(4);
            foreach (var 课 in 各课)
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
            有贼否 = 下贼上课.Count is not 0;
            return 有贼否 ? 下贼上课 : 上克下课;
        }
        private static IEnumerable<四课之一> 取上比者(IEnumerable<四课之一> 各课, YinYang 日阴阳)
        {
            return 各课.ToLookup((课) => 课.上阴阳)[日阴阳];
        }

        private static readonly ILookup<EarthlyBranch, FiveElement> 逆寄宫五行 =
            Enumerable.Range(1, 10)
            .Select(item => new HeavenlyStem(item))
            .ToLookup(stem => stem.寄宫(), stem => stem.五行());
        private static IReadOnlyList<四课之一> 上克下比较涉害程度取(IEnumerable<四课之一> 各课)
        {
            int 最大涉害程度 = 0;
            List<四课之一> 结果 = new(4);
            foreach (var 课 in 各课)
            {
                Debug.Assert(
                    课.下五行.GetRelationship(课.上五行) is FiveElementsRelationship.OvercomingMe);

                int 此次涉害程度 = 0;
                for (var current = 课.支下或干下之寄宫; current != 课.上; current = current.Next())
                {
                    此次涉害程度 += 逆寄宫五行[current].Append(current.五行())
                        .Where(low => 课.上五行.GetRelationship(low) is FiveElementsRelationship.OvercameByMe)
                        .Count();
                }
                if (此次涉害程度 > 最大涉害程度)
                {
                    最大涉害程度 = 此次涉害程度;
                    结果.Clear();
                }
                if (此次涉害程度 == 最大涉害程度)
                    结果.Add(课);
            }
            return 结果;
        }
        private static IReadOnlyList<四课之一> 下贼上比较涉害程度取(IEnumerable<四课之一> 各课)
        {
            int 最大涉害程度 = 0;
            List<四课之一> 结果 = new(4);
            foreach (var 课 in 各课)
            {
                Debug.Assert(
                    课.上五行.GetRelationship(课.下五行) is FiveElementsRelationship.OvercomingMe);

                int 此次涉害程度 = 0;
                for (var current = 课.支下或干下之寄宫; current != 课.上; current = current.Next())
                {
                    此次涉害程度 += 逆寄宫五行[current].Append(current.五行())
                        .Where(low => 课.上五行.GetRelationship(low) is FiveElementsRelationship.OvercomingMe)
                        .Count();
                }
                if (此次涉害程度 > 最大涉害程度)
                {
                    最大涉害程度 = 此次涉害程度;
                    结果.Clear();
                }
                if (此次涉害程度 == 最大涉害程度)
                    结果.Add(课);
            }
            return 结果;
        }

        private static IEnumerable<四课之一> 按下孟仲取(IEnumerable<四课之一> 各课)
        {
            var lookup = 各课.ToLookup((课) => 课.支下或干下之寄宫.获取孟仲季());

            if (lookup.Contains(孟仲季.孟))
                return lookup[孟仲季.孟];
            else if (lookup.Contains(孟仲季.仲))
                return lookup[孟仲季.仲];
            else
                return Enumerable.Empty<四课之一>();
        }
        private static IReadOnlyList<四课之一> 取遥克(
            IEnumerable<四课之一> 各课, FiveElement 日五行, out bool 是蒿矢或弹射)
        {
            var 蒿矢 = new List<四课之一>(4);
            var 弹射 = new List<四课之一>(4);
            foreach (var 课 in 各课)
            {
                switch (日五行.GetRelationship(课.上五行))
                {
                    case FiveElementsRelationship.OvercomingMe:
                        蒿矢.Add(课);
                        break;
                    case FiveElementsRelationship.OvercameByMe:
                        弹射.Add(课);
                        break;
                }
            }
            是蒿矢或弹射 = 蒿矢.Count is not 0;
            return 是蒿矢或弹射 ? 蒿矢 : 弹射;
        }
        #endregion
    }
}