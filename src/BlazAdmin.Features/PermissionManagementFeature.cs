using BlazAdmin.Abstract;
using System;
using System.Collections.Generic;

namespace BlazAdmin.Features
{
    public class PermissionManagementFeature : IFeature
    {
        public List<MenuModel> Menus { get; } = new List<MenuModel>();

        public PermissionManagementFeature()
        {
            var permissionMenu = new MenuModel();
            permissionMenu.Label = "权限管理";
            permissionMenu.Name = "权限管理";
            permissionMenu.Icon = "el-icon-lock";
            permissionMenu.Children.Add(new MenuModel()
            {
                Icon = "el-icon-user-solid",
                Label = "用户列表",
                Route = "/user/list",
                Name = "userlist",
                Title = "用户列表"
            });
            permissionMenu.Children.Add(new MenuModel()
            {
                Icon = "el-icon-s-custom",
                Label = "角色列表",
                Route = "/user/roles",
                Name = "rolelist",
                Title = "角色列表"
            });
            permissionMenu.Children.Add(new MenuModel()
            {
                Icon = "el-icon-s-grid",
                Label = "功能列表",
                Name = "featurelist",
                Route = "/user/features",
                Title = "功能列表"
            });
            Menus.Add(permissionMenu);
        }
    }
}
