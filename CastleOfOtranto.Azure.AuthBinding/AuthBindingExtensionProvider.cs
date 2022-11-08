using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;

namespace CastleOfOtranto.Azure.AuthBinding
{
    [Extension("AuthBinding")]
    public class AuthBindingExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration _conf;

        public AuthBindingExtensionProvider(IConfiguration configuration)
        {
            _conf = configuration;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<AuthAttribute>()
                .Bind(new AuthBindingProvider(_conf.GetValue<string>("Authority"),
                    _conf.GetValue<string>("Audience")));
        }
    }
}

