using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.起课信息;
using SixRens.Api.工具;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.神煞
{
    internal sealed partial class 神煞六壬辨疑神煞纪要实现 : I神煞插件.I神煞内容提供器
    {
        private static readonly Dictionary<string, 取神煞法> 取神煞法列表 = new();
        private static readonly Dictionary<string, 取多神煞法> 取多神煞法列表 = new();
        static 神煞六壬辨疑神煞纪要实现()
        {
            foreach (var p in typeof(取神煞法提供类).GetProperties())
            {
                var r = p.GetValue(null);
                Debug.Assert(r is 取神煞法 or 取多神煞法);
                if (r is 取神煞法 单)
                    取神煞法列表.Add(p.Name, 单);
                else if (r is 取多神煞法 多)
                    取多神煞法列表.Add(p.Name, 多);
            }
        }

        public static IEnumerable<string> 支持的神煞 =>
            取神煞法列表.Keys.Concat(取多神煞法列表.Keys);

        private readonly 壬式信息 壬式;

        internal 神煞六壬辨疑神煞纪要实现(I年月日时 年月日时)
        {
            this.壬式 = new(年月日时);
        }
        
        public IEnumerable<EarthlyBranch> 取所在神(string 神煞名)
        {
            if (取神煞法列表.TryGetValue(神煞名, out var 取单法))
            {
                var 结果 = 取单法(this.壬式);
                if (结果.HasValue)
                    return new[] { 结果.Value };
                else
                    return Array.Empty<EarthlyBranch>();
            }

            if (取多神煞法列表.TryGetValue(神煞名, out var 取多法))
            {
                var 结果 = 取多法(this.壬式);
                return 结果;
            }

            throw new 起课失败异常($"不支持的神煞题目：{神煞名}");
        }
    }
}
