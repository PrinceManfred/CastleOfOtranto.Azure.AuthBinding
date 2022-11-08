using System;
using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;

[assembly: ExtensionInformation("CastleOfOtranto.Azure.AuthBinding", "0.0.3")]

namespace CastleOfOtranto.Azure.AuthBinding.Isolated
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class AuthInputAttribute : InputBindingAttribute
    {
        public AuthInputAttribute()
        {
            Scopes = Array.Empty<string>();
        }

        public AuthInputAttribute(string[] scopes)
        {
            Scopes = scopes;
        }

        public string[] Scopes { get; set; }
    }
}

