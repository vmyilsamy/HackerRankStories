using BestNStories.Interfaces;
using BestNStories.Response;
using BestNStories.Services;
using Given.Common;
using Given.NUnit;
using Moq;
using NUnit.Framework;

namespace BestNStories.UnitTests;

[TestFixture]
public class HackerNewsService_get_ids_for_best_stories_from_cache  : Scenario
{
    private static StoryIdCollection _storyIds;
    private static IHackerNewsService _hackerNewsService;
    private static Mock<IHackerNewsClient> _hackerNewsClientMock;
    private static Mock<ICache> _cacheMock;
    private static string cacheKey = "BestStoriesIdsCacheKey";

    given there_are_stories_found_in_cache = () => {

        _hackerNewsClientMock = new Mock<IHackerNewsClient>();

        _cacheMock = new Mock<ICache>();

        _cacheMock.Setup(cache => cache.Get<StoryIdCollection>(cacheKey)).Returns(new StoryIdCollection { 1, 2, 3, 4 });

        _hackerNewsService = new HackerNewsService(_hackerNewsClientMock.Object, _cacheMock.Object);
    };

    when I_request_ids_for_best_stories = () => {

        _storyIds = _hackerNewsService.GetIdsForBestStories().GetAwaiter().GetResult();

    };

    [then]
    public void should_not_get_ids_from_hackernews_client()
    {
        _hackerNewsClientMock.Verify(client => client.GetIdsForBestStories(), Times.Never);
    }

    [then]
    public void should_not_add_ids_to_the_cache()
    {
        _cacheMock.Verify(cache => cache.Add(It.IsAny<string>(), It.IsAny<StoryIdCollection>()), Times.Never);
    }
}