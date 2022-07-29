using SixRens.Core.插件管理.插件包管理;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台效果测试插件法
{
    internal sealed class 存储器 : I插件包管理器储存器
    {
        public void 储存插件包文件(string 插件包名, Stream 插件包)
        {
            return;
        }

        public string 生成新的插件包名()
        {
            return Path.GetRandomFileName();
        }

        public void 移除插件包文件(string 插件包名)
        {
            return;
        }

        public IEnumerable<(string 插件包名, Stream 插件包)> 获取所有插件包文件()
        {
            yield break;
        }
    }
}
