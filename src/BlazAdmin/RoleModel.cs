using Blazui.Component.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class RoleModel
    {
        public string Id { get; set; }

        [TableColumn(Text = "名称")]
        public string Name { get; set; }

        public List<string> Resources { get; set; }
    }
}
