using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestNStories.Interfaces;
using BestNStories.Response;

namespace BestNStories.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string BestStoriesIdsCacheKey = "BestStoriesIdsCacheKey";

        private readonly IHackerNewsClient _hackerNewsClient;
        private readonly ICache _cache;
        private readonly ParallelOptions options;

        public HackerNewsService(IHackerNewsClient hackerNewsClient, ICache cache)
        {
            _hackerNewsClient = hackerNewsClient;
            _cache = cache;
            options = new ParallelOptions() { MaxDegreeOfParallelism = 20 };
        }

        public async Task<StoryIdCollection> GetIdsForBestStories()
        {
            var bestStoryIds = _cache.Get<StoryIdCollection>(BestStoriesIdsCacheKey);

            if (bestStoryIds == null)
            {
                bestStoryIds = await _hackerNewsClient.GetIdsForBestStories();

                await _cache.Add(BestStoriesIdsCacheKey, bestStoryIds);
            }

            return bestStoryIds;
        }

        public async Task<IEnumerable<Story>> GetStories(IEnumerable<int> storyIds)
        {
            var bestStories = new List<Story>();

            await Parallel.ForEachAsync(storyIds, options, async (storyId, ct) =>
            {
                var story = _cache.Get<Story>(storyId);

                if (story == null)
                {
                    story = await _hackerNewsClient.GetStoryById(storyId);

                    await _cache.Add(storyId, story);
                }

                bestStories.Add(story);
            });

            var storiesInDescendingOrderByScore = bestStories.OrderByDescending(story => story.Score);

            return storiesInDescendingOrderByScore;
        }
    }
}