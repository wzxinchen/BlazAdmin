using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    /// <summary>
    /// 为组件指定资源名
    /// </summary>
    public class ResourceAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
