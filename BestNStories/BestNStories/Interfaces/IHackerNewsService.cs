using System.Collections.Generic;
using System.Threading.Tasks;
using BestNStories.Response;

namespace BestNStories.Interfaces
{
    public interface IHackerNewsService
    {
        Task<StoryIdCollection> GetIdsForBestStories();
        Task<IEnumerable<Story>> GetStories(IEnumerable<int> storyIds);
    }
}