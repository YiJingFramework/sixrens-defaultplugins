namespace 打包程序
{
    internal static class 打包参数
    {
        public static readonly 插件包信息 信息 = new(
            名称: "SixRens.DefaultPlugins",
            版本号: $"{typeof(SixRens.DefaultPlugins.三传.三传涉害深浅打表式).Assembly.GetName().Version?.ToString(3)}",
            网址: "https://github.com/YiJingFramework/sixrens-defaultplugins.git",
            主程序集: $"SixRens.DefaultPlugins.dll",
            插件类: new Type[] {
               typeof(SixRens.DefaultPlugins.三传.三传涉害深浅打表式),
               // typeof(SixRens.DefaultPlugins.三传.三传涉害深浅计算式),
               typeof(SixRens.DefaultPlugins.天将.天将甲戊庚牛羊壬癸蛇兔藏),
               typeof(SixRens.DefaultPlugins.神煞.神煞六壬辨疑神煞纪要),
               typeof(SixRens.DefaultPlugins.课体.课体六壬辨疑卷二),
               typeof(SixRens.DefaultPlugins.参考.参考观易吟),
            });
        public static readonly string[] 包含的程序集 = new[]
        {
            "SixRens.DefaultPlugins.dll",
            "SixRens.Tools.dll",
            "YiJingFramework.Core.dll",
            "YiJingFramework.FiveElements.dll"
        };
    }
}
