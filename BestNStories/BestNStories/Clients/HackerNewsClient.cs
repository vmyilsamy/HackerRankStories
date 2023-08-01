using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BestNStories.Interfaces;
using BestNStories.Response;

namespace BestNStories.Clients
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;

        public HackerNewsClient(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("hackerRankClient");
        }

        public async Task<StoryIdCollection> GetIdsForBestStories()
        {
            string requestUri = "v0/beststories.json";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var responseMessage = await _httpClient.SendAsync(request);

            responseMessage.EnsureSuccessStatusCode();

            var content = await responseMessage.Content.ReadAsStringAsync();
            
            var bestStoryIds = JsonSerializer.Deserialize<StoryIdCollection>(content);

            return bestStoryIds ?? new StoryIdCollection();
        }

        public async Task<Story> GetStoryById(int storyId)
        {
            string requestUri = $"v0/item/{storyId}.json";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var responseMessage = await _httpClient.SendAsync(request);

            responseMessage.EnsureSuccessStatusCode();

            var content = await responseMessage.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreReadOnlyProperties = true
            };
            var story = JsonSerializer.Deserialize<Story>(content, options);

            return story ?? new EmptyStory(storyId);
        }
    }
}