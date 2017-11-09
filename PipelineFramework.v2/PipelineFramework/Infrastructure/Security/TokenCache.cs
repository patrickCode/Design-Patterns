using System;
using System.Collections.Generic;

namespace PipelieneFramework.Instrastructure.Security
{
    public class TokenCache
    {
        public Dictionary<string, string> _internalCache;
        public Dictionary<string, DateTime> _tokenExpirationTime;

        public TokenCache()
        {
            _internalCache = new Dictionary<string, string>();
            _tokenExpirationTime = new Dictionary<string, DateTime>();
        }

        public string GetToken(string resourceId)
        {
            if (_internalCache.ContainsKey(resourceId) && _tokenExpirationTime.ContainsKey(resourceId))
            {
                var expirationTime = _tokenExpirationTime[resourceId];
                if (expirationTime < DateTime.UtcNow.AddMinutes(-2))
                    return _internalCache[resourceId];
                else
                {
                    _tokenExpirationTime.Remove(resourceId);
                    _internalCache.Remove(resourceId);
                }

            }
            return null;
        }

        public void CacheToken(string resourceId, string token, DateTime expirationTime)
        {
            if (_internalCache.ContainsKey(resourceId))
                _internalCache.Remove(resourceId);
            if (_tokenExpirationTime.ContainsKey(resourceId))
                _internalCache.Remove(resourceId);

            _internalCache.Add(resourceId, token);
            _tokenExpirationTime.Add(resourceId, expirationTime);
        }
    }
}