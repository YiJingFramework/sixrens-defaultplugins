﻿using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;

namespace SixRens.DefaultPlugins.三传
{
    public sealed class 三传涉害深浅计算式 : I三传插件
    {
        public string? 插件名 => "涉害深浅计算";

        public Guid 插件识别码 { get; } = new Guid("F53253DC-099B-412A-8B14-6C59BE64DA38");

        public I三传 获取三传(I起课信息 起课信息, I天地盘 天地盘, I四课 四课)
        {
            return new 三传涉害深浅打表式实现(四课, 天地盘);
        }
    }
}
