namespace WantToSell.Application.Contracts.Cache;

public interface ICacheHelper
{
    Task<T> GetOrSet<T>(string key, Func<Task<T>> queryFunction, TimeSpan? expiry = null);
    Task<T> Get<T>(string key);
    Task Set<T>(string key, T value, TimeSpan? expiry = null);
    Task<bool> Remove(string key);
}