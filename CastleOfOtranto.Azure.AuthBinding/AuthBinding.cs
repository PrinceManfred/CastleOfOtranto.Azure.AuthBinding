using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CastleOfOtranto.Azure.AuthBinding
{
    public class AuthBinding : IBinding
    {
        private readonly AuthAttribute _attr;
        private readonly string _audience;
        private readonly JwtSecurityTokenHandler _tokenValidator;
        private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        public AuthBinding(AuthAttribute attr, string defaultAuthority, string audience)
        {
            _attr = attr;
            _audience = audience;

            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{defaultAuthority}/.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever());

            _tokenValidator = new JwtSecurityTokenHandler();
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            var request = (HttpRequest)context.BindingData["$request"];

            if (request is null)
            {
                return new AuthValueProvider(new AuthResult());
            }

            if (request.Headers is null
                || request.Headers["Authorization"].Count != 1)
            {
                return new AuthValueProvider(new AuthResult());
            }

            var bearerToken = request.Headers["Authorization"][0].
                Replace("Bearer ", "");

            if (!_tokenValidator.CanReadToken(bearerToken))
            {
                return new AuthValueProvider(new AuthResult());
            }

            var openIdConfig = await _configurationManager.GetConfigurationAsync(default);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = _audience,
                ValidIssuer = openIdConfig.Issuer,
                IssuerSigningKeys = openIdConfig.SigningKeys
            };
            
            try
            {
                var principal = _tokenValidator.ValidateToken(
                        bearerToken, tokenValidationParameters, out _);

                var authResult = new AuthResult(true, true, bearerToken, principal);
                return new AuthValueProvider(authResult);
            }
            catch (SecurityTokenException)
            {
                return new AuthValueProvider(new AuthResult());
            }
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor();
        }
    }
}

