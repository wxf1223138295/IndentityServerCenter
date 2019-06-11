using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApiServerOneNew.Model
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
