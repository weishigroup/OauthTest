using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSource
{
    public class PermissionAuthorizeAttribute : Attribute, IPermissionAuthorizeData
    {
        public string Project { set; get; }
        public string Policy { set; get; }
        public string Roles { set; get; }
        public string AuthenticationSchemes { set; get; }
        
    }
}
