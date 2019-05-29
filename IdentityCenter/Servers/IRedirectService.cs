using System;

namespace IdentityCenter.Servers
{
    public interface IRedirectService
    {
         string ExtractRedirectUriFromReturnUrl(string url);
    }
}
