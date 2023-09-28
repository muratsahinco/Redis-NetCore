namespace Example.Common.Cache.Model
{
    public class RedisSetting
    {
        public string RedisEndPoint { get; set; }
        public string RedisPort { get; set; }
        public string RedisPassword { get; set; }
        public int RedisDB { get; set; }
    }
}
