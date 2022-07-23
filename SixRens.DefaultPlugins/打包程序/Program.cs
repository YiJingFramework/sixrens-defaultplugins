using SixRens.Api.工具;
using System.IO.Compression;
using 打包程序;

var 文件信息 = new FileInfo($"{打包参数.信息.名称}-{打包参数.信息.版本号}.srspg");

using FileStream 文件 = new FileStream(文件信息.FullName, FileMode.Create);
using var 压缩包 = new ZipArchive(文件, ZipArchiveMode.Create);
using (StreamWriter 信息项目 =
    new(压缩包.CreateEntry("plugin.json").Open(), leaveOpen: false))
{
    信息项目.Write(打包参数.信息.生成插件包信息文件内容());
}
foreach (var 程序集 in 打包参数.包含的程序集)
{
    _ = 压缩包.CreateEntryFromFile(程序集, 程序集);
}

Console.WriteLine($"已打包到 {文件信息.FullName}");
