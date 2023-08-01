using System.Threading.Tasks;
using BestNStories.Response;

namespace BestNStories.Interfaces
{
    public interface IHackerNewsClient
    {
        Task<StoryIdCollection> GetIdsForBestStories();
        Task<Story> GetStoryById(int storyId);
    }
}