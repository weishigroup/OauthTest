using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSource
{
    interface IPermissionAuthorizeData: IAuthorizeData
    {
         string Project { set; get; }
    }
}
