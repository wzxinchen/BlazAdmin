using Blazui.Component.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class UserModel
    {
        public string Id { get; set; }
        [TableColumn(Text = "用户名")]
        public string Username { get; set; }

        [TableColumn(Text = "邮箱")]
        public string Email { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
