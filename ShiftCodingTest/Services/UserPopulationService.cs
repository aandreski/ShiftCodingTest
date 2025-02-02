using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ShiftCodingTest.Models;

namespace ShiftCodingTest.Services
{
    public class UserPopulationService : IUserPopulationService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "UserCache";

        public UserPopulationService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<List<User>?> GetUsersAsync()
        {
            //if already in memory, get from cache
            if (_memoryCache.TryGetValue(CacheKey, out List<User>? users))
                return users ?? [];

            //otherwise, populate cache
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json) ?? [];

            _memoryCache.Set(CacheKey, userList, TimeSpan.FromMinutes(10));

            return userList;
        }
    }
}
