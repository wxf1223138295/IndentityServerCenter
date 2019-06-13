using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MVCClientHybrid.Models
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
