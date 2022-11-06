using System;
using Microsoft.Azure.WebJobs.Description;

namespace CastleOfOtranto.Azure.AuthBinding
{
    
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class AuthAttribute : Attribute
	{
        public AuthAttribute()
        {
            Scopes = Array.Empty<string>();
        }

        public AuthAttribute(string[] scopes)
		{
            Scopes = scopes;
		}

        public string[] Scopes { get; set; }
    }
}

