using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazAdmin.Docs.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        protected List<MenuModel> Menus { get; set; } = new List<MenuModel>();

        protected override void OnInitialized()
        {
            Menus.Add(new MenuModel()
            {
                Label = "快速上手",
                Children = new List<MenuModel>() {
                  new MenuModel(){
                   Label="Blazui 入门",
                   Route="/guide/blazui"
                  },
                     new MenuModel(){
                   Label="BlazAdmin 入门",
                   Route="/guide/blazadmin"
                  }
                 }
            });
            Menus.Add(new MenuModel()
            {
                Label = "基础组件",
                Children = new List<MenuModel>() {
                  new MenuModel(){
                   Label="Button 按钮",
                   Route="/docs/button"
                  },
                     new MenuModel(){
                   Label="Input 输入框",
                   Route="/docs/input"
                  },
                     new MenuModel(){
                   Label="Radio 单选框",
                   Route="/docs/radio"
                  },
                     new MenuModel(){
                   Label="Checkbox 多选框",
                   Route="/docs/checkbox"
                  },
                     new MenuModel(){
                   Label="Switch",
                   Route="/docs/switch"
                  },
                     new MenuModel(){
                   Label="Select 选择器",
                   Route="/docs/select"
                  },
                     new MenuModel(){
                   Label="NavMenu 导航菜单",
                   Route="/docs/menu"
                  },
                     new MenuModel(){
                   Label="Pagination 分页",
                   Route="/docs/pagination"
                  },
                     new MenuModel(){
                   Label="Tabs 标签页",
                   Route="/docs/tabs"
                  },
                     new MenuModel(){
                   Label="Table 表格",
                   Route="/docs/table"
                  },
                     new MenuModel(){
                   Label="Form 表单",
                   Route="/docs/form"
                  },
                     new MenuModel(){
                   Label="DatePicker 日期选择器",
                   Route="/docs/datepicker"
                  }
                 }
            });
            Menus.Add(new MenuModel()
            {
                Label = "弹窗组件",
                Children = new List<MenuModel>() {
                  new MenuModel(){
                   Label="Message 消息",
                   Route="/docs/message"
                  },
                     new MenuModel(){
                   Label="Loading 加载中",
                   Route="/docs/loading"
                  },
                     new MenuModel(){
                   Label="MessageBox 消息弹窗",
                   Route="/docs/messagebox"
                  },
                     new MenuModel(){
                   Label="Dialog 对话框",
                   Route="/docs/dialog"
                  }
                 }
            });
        }
    }
}
