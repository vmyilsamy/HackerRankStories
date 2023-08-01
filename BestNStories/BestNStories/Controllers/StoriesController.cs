using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BestNStories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BestNStories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }


        [HttpGet(Name = "GetStories")]
        public async Task<IActionResult> GetStories(int numberOfStoriesRequested)
        {
            var bestStoryIds = await _hackerNewsService.GetIdsForBestStories();

            if(numberOfStoriesRequested > bestStoryIds.Count)
            {
                throw new HttpRequestException($"Number of stories requested({numberOfStoriesRequested}) was beyond the limit {bestStoryIds.Count}. Please request below or upto the limit");
            }

            var takeStoryIdsUpto = bestStoryIds.Take(numberOfStoriesRequested);

            var stories = await _hackerNewsService.GetStories(takeStoryIdsUpto);

            return Ok(stories);
        }
    }
}