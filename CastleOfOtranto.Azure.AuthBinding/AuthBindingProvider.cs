using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace CastleOfOtranto.Azure.AuthBinding
{
    public class AuthBindingProvider : IBindingProvider
    {
        private readonly string _authority;
        private readonly string _audience;

        public AuthBindingProvider(string authority, string audience)
        {
            _authority = authority;
            _audience = audience;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            var attr = context.Parameter.GetCustomAttribute<AuthAttribute>(false);
            
            return Task.FromResult((IBinding)new AuthBinding(attr, _authority, _audience));
        }
    }
}

