using BestNStories.Interfaces;
using BestNStories.Response;
using BestNStories.Services;
using Given.Common;
using Given.NUnit;
using Moq;
using NUnit.Framework;

namespace BestNStories.UnitTests
{
    [TestFixture]
    public class HackerNewsService_get_ids_for_best_stories_from_service_client  : Scenario
    {
        private static StoryIdCollection _storyIds;
        private static IHackerNewsService _hackerNewsService;
        private static Mock<IHackerNewsClient> _hackerNewsClientMock;
        private static Mock<ICache> _cacheMock;
        private string cacheKey = "BestStoriesIdsCacheKey";

        given there_are_no_stories_found_in_cache = () => {

            _hackerNewsClientMock = new Mock<IHackerNewsClient>();

            _cacheMock = new Mock<ICache>();

            _hackerNewsClientMock.Setup(client => client.GetIdsForBestStories()).ReturnsAsync(new StoryIdCollection() { 1, 2, 3, 4 });

            _hackerNewsService = new HackerNewsService(_hackerNewsClientMock.Object, _cacheMock.Object);
        };

        when I_request_ids_for_best_stories = () => {

           _storyIds = _hackerNewsService.GetIdsForBestStories().GetAwaiter().GetResult();

        };
        

        [then]
        public void should_get_ids_from_hackernews_client()
        {
            _hackerNewsClientMock.Verify(client => client.GetIdsForBestStories(), Times.Once);
        }

        [then]
        public void should_add_ids_to_the_cache()
        {
            _cacheMock.Verify(cache => cache.Add(It.Is<string>(key => key == cacheKey), It.Is<StoryIdCollection>(storyIds => !_storyIds.Except(storyIds).Any())), Times.Once);
        }
    }
}