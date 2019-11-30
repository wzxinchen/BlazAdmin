using Blazui.Component.Input;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin
{
    public class BLoginBase : ComponentBase
    {
        [Parameter]
        public LoginModel DefaultUser { get; set; }
        protected InputType passwordType = InputType.Password;
        internal void TogglePassword()
        {
            if (passwordType == InputType.Password)
            {
                passwordType = InputType.Text;
            }
            else
            {
                passwordType = InputType.Password;
            }
        }


    }
}
