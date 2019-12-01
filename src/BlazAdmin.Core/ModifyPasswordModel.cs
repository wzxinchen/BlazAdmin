using System;
using System.Collections.Generic;
using System.Text;

namespace BlazAdmin.Core
{
    internal class ModifyPasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
