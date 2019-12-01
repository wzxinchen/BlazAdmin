using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Core
{
    public class MenuModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public List<MenuModel> Children { get; set; } = new List<MenuModel>();
        public string Icon { get; set; }
    }
}
