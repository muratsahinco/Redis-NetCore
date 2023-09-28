using Example.Common.Cache.Service;
using Microsoft.AspNetCore.Mvc;

namespace NetCore_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : Controller
    {
        private readonly ICacheService _cacheService;
        public RedisController(ICacheService cacheService) => _cacheService = cacheService;


        [HttpGet("GetRedisData")]
        public async Task<IActionResult> GetRedisData(string KeyData)
        {
            var redisValue = _cacheService.Get<string>(KeyData);
            return Ok(redisValue);
        }

        [HttpPost("AddRedisData")]
        public async Task<IActionResult> AddRedisData(string KeyData, string KeyValue)
        {
            _cacheService.Add(KeyData, KeyValue);
            return Ok();
        }

        [HttpPut("UpdateRedisData")]
        public async Task<IActionResult> UpdateRedisData(string KeyData, string KeyValue)
        {
            // update için ytine Add kullanabiliriz. Değer var ise güncelleyecektir.
            _cacheService.Add(KeyData, KeyValue);
            return Ok();
        }

        [HttpDelete("DeleteRedisData")]
        public async Task<IActionResult> DeleteRedisData(string KeyData, string KeyValue)
        {
            _cacheService.Remove(KeyData);
            return Ok();
        }
    }
}
