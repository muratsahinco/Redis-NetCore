namespace Example.Common.Cache.Service
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Add(string key, object data);
        void Remove(string key);
        bool Any(string key);
    }
}
