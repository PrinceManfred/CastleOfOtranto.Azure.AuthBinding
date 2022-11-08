using System;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace CastleOfOtranto.Azure.AuthBinding
{
	public struct AuthResult
	{
        public AuthResult(bool isAuthenticated, bool isAuthorized, string jwtToken, ClaimsPrincipal principal)
        {
            IsAuthenticated = isAuthenticated;
            IsAuthorized = isAuthorized;
            JwtToken = jwtToken;
            Principal = principal;
        }

        public bool IsAuthenticated { get; }
		public bool IsAuthorized { get; }
		public string JwtToken { get; }
		public ClaimsPrincipal Principal { get; }
	}
}