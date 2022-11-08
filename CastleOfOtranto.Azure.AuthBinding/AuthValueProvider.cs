using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace CastleOfOtranto.Azure.AuthBinding
{
    public class AuthValueProvider : IValueProvider
    {
        private readonly AuthResult _result;

        public AuthValueProvider(AuthResult result)
        {
            _result = result;
        }

        public Type Type => typeof(AuthResult);

        public Task<object> GetValueAsync()
        {
            return Task.FromResult((object)_result);
        }

        public string ToInvokeString()
        {
            return "Auth";
        }
    }
}

