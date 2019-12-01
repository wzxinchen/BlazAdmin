using BlazAdmin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlazAdmin.Core
{
    public class BUserManagementBase : BAdminPageBase
    {
        protected List<UserModel> Users { get; private set; } = new List<UserModel>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
