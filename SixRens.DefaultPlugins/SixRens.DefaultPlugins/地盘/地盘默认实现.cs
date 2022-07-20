using SixRens.Api.实体.壬式;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.StemsAndBranches;

namespace SixRens.DefaultPlugins.地盘
{
    internal sealed class 地盘默认实现 : I地盘
    {
        public EarthlyBranch 取地支(EarthlyBranch 位置)
        {
            return 位置;
        }
    }
}
