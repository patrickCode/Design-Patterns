using System.Threading.Tasks;
using PipelineFramework.Common.Spec;
using PipelineFramework.Common.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace PipelieneFramework.Instrastructure.Security
{
    public class AuthContext: IAuthContext
    {
        private readonly TokenCache _cache;
        private readonly AuthenticationConfiguration _configuration;

        public AuthContext(TokenCache cache, AuthenticationConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<string> GetTokenUsingSecret(string resourceId)
        {
            var cachedToken = _cache.GetToken(resourceId);
            if (cachedToken != null)
                return cachedToken;

            var authority = $"https://login.microsoftonline.com/{_configuration.TenantId}";
            var context = new AuthenticationContext(authority);
            var clientCredential = new ClientCredential(_configuration.ClientId, _configuration.ClientSecret);

            var result = await context.AcquireTokenAsync(resourceId, clientCredential);
            var token = result.AccessToken;

            _cache.CacheToken(resourceId, token, result.ExpiresOn.UtcDateTime);
            return token;
        }
    }
}