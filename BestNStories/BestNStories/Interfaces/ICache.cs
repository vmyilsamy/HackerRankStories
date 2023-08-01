using System.Threading.Tasks;

namespace BestNStories.Interfaces
{
    public interface ICache
    {
        T Get<T>(object cacheKey);
        Task Add<T>(object cacheKey, T item);
    }
}