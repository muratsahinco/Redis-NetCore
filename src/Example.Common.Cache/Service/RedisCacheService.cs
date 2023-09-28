using System.Text.Json;
using Example.Common.Cache.Helper;

namespace Example.Common.Cache.Service
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;
        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data)
        {
            if (!key.Contains("YourProjectName:"))
                key = "YourProjectName:" + key;

            string jsonData = "";
            if (data is string s)
                jsonData = s;
            else
                jsonData = JsonSerializer.Serialize(data);

            var timeSpanExpire = GetTimeSpan(2);
            if (_redisServer.IsConnect)
                _redisServer.Database.StringSet(key, jsonData, timeSpanExpire);
        }

        public bool Any(string key)
        {
            if (!key.Contains("YourProjectName:"))
                key = "YourProjectName:" + key;

            if (_redisServer.IsConnect)
                return _redisServer.Database.KeyExists(key);
            else return false;
        }

        public T Get<T>(string key)
        {
            if (!key.Contains("YourProjectName:"))
                key = "YourProjectName:" + key;

            if (Any(key) && _redisServer.IsConnect)
            {
                string jsonData = _redisServer.Database.StringGet(key);
                if (typeof(T) == typeof(string))
                    return (T)(object)jsonData;
                else
                    return JsonSerializer.Deserialize<T>(jsonData);
            }

            return default;
        }

        public void Remove(string key)
        {
            if (!key.Contains("YourProjectName:"))
                key = "YourProjectName:" + key;

            if (_redisServer.IsConnect)
                _redisServer.Database.KeyDelete(key);
        }

        private TimeSpan GetTimeSpan(int Hour)
        {
            return new TimeSpan(0, Hour, 0, 0);
        }
    }
}
