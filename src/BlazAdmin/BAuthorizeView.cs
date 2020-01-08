using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BAuthorizeView : AuthorizeView
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (NotAuthorized == null)
            {
                NotAuthorized = provider =>
                {
                    return builder =>
                    {
                        builder.AddContent(0, "您没有权限访问该页面");
                    };
                };
            }
        }
    }
}
