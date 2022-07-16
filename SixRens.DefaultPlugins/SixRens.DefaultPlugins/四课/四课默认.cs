﻿using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.DefaultPlugins.工具;

namespace SixRens.DefaultPlugins.四课
{
    public sealed class 四课默认 : I四课插件
    {
        public string? 插件名 => "默认";

        public Guid 插件识别码 => new Guid("6BAD32BC-0951-417D-B01B-53623FA98D8D");

        public I四课 获取四课(I年月日时 年月日时, 地支盘 基础盘, 地支盘 地盘, 地支盘 天盘)
        {
            return new 四课默认实现(年月日时, new 天地盘(地盘, 天盘));
        }
    }
}
