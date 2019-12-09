using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace BlazAdmin.Core
{
    public class BUserManagementBase : BAdminPageBase
    {
        protected List<object> Users { get; private set; } = new List<object>();


        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            Users = await UserService.GetUsersAsync();
        }
        public void Edit(object user)
        {

        }
        public void Del(object user)
        {
            UserService.DeleteUserAsync(user)
        }
    }
}
