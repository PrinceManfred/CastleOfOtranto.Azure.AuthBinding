using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;

namespace CastleOfOtranto.Azure.AuthBinding
{
    [Extension("AuthBinding")]
    public class AuthBindingExtensionProvider : IExtensionConfigProvider
    {
        public AuthBindingExtensionProvider()
        {
        }

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<AuthAttribute>()
                .BindToInput<string>(BindMe);
        }

        public Task<string> BindMe(AuthAttribute attr, ValueBindingContext ctx)
        {
            return Task.FromResult<string>("Hello!");
        }
    }
}

