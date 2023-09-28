using Example.Common.Cache.Model;
using StackExchange.Redis;

namespace Example.Common.Cache.Helper
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;

        public RedisServer(RedisSetting redisSetting)
        {
            try
            {
                _connectionMultiplexer = ConnectionMultiplexer.Connect($"{redisSetting.RedisEndPoint}:{redisSetting.RedisPort},password={redisSetting.RedisPassword}");
                IsConnect = true;
            }
            catch (Exception e)
            {
            }

            if (IsConnect)
                _database = _connectionMultiplexer.GetDatabase(redisSetting.RedisDB);
        }
        public IDatabase Database => _database;
        public bool IsConnect = false;
    }
}
