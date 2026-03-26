using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using toolbox.Models;
using cache1 = Microsoft.Extensions.Caching.Memory;
using cache2 = System.Runtime.Caching;

var employee = new Employee()
{
    EmployeeId = 1,
    EmployeeName = "Charlie"
};

var guid = Guid.NewGuid().ToString();

// microsoft caching
var options = new MemoryCacheOptions();
var cache1 = new cache1.MemoryCache(options);
cache1.Set(guid, employee);

// system caching
var cache2 = new cache2.MemoryCache("CompanyEmployee2");
var cacheItem = new CacheItem(Guid.NewGuid().ToString(), employee);
var cacheItemPolicy = new CacheItemPolicy
{
    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0)
};
cache2.Add(cacheItem, cacheItemPolicy);