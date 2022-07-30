using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens.DefaultPlugins.神煞
{
    public sealed class 神煞六壬辨疑神煞纪要 : I神煞插件
    {
        public string? 插件名 => "六壬辨疑神煞纪要";

        public Guid 插件识别码 => new Guid("59375F38-6BE3-46AC-8C46-F4BBF0C03382");

        public IEnumerable<I神煞题目> 支持的神煞 => 神煞六壬辨疑神煞纪要实现.支持的神煞;

        public I神煞内容 获取神煞(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, string 神煞题目)
        {
            return 神煞六壬辨疑神煞纪要实现.取煞(年月日时, 神煞题目);
        }
    }
}
