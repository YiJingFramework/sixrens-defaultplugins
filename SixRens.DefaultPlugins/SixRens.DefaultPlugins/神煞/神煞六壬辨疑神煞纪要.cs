using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.神煞
{
    public sealed class 神煞六壬辨疑神煞纪要 : I神煞插件
    {
        public string? 插件名 => "六壬辨疑神煞纪要";

        public Guid 插件识别码 { get; } = new Guid("59375F38-6BE3-46AC-8C46-F4BBF0C03382");

        public IEnumerable<string> 支持神煞的名称 => 神煞六壬辨疑神煞纪要实现.支持的神煞;

        public I神煞插件.I神煞内容提供器 获取神煞内容提供器(I起课信息 起课信息, I天地盘 天地盘, I四课 四课, I三传 三传, I天将盘 天将盘)
        {
            return new 神煞六壬辨疑神煞纪要实现(起课信息.年月日时);
        }
    }
}
