using System;
using CastleOfOtranto.Azure.AuthBinding;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(AuthBindingStartup))]

namespace CastleOfOtranto.Azure.AuthBinding
{
	public class AuthBindingStartup : IWebJobsStartup
    {
		public AuthBindingStartup()
		{
		}

        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<AuthBindingExtensionProvider>();
        }
    }
}

