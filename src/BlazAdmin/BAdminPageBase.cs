using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BAdminPageBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
    }
}
