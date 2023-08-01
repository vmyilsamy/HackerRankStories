using System;
using System.Net.Http;
using BestNStories.Clients;
using BestNStories.Components;
using BestNStories.Interfaces;
using BestNStories.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestNStories.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureComponents(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();

            serviceCollection.AddSingleton<ICache, Cache>();

            string baseUri = configuration.GetValue<string>("HackerRankClientSettings:BaseUrl");

            string host = configuration.GetValue<string>("HackerRankClientSettings:Host");

            serviceCollection
                .AddHttpClient<HttpClient>("hackerRankClient", client => { 
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Add("Host", host);
                    client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                    client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "Windows");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                    client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseDefaultCredentials = true });

            serviceCollection.AddScoped<IHackerNewsClient, HackerNewsClient>();
            
            serviceCollection.AddScoped<IHackerNewsService, HackerNewsService>();
        }
    }
}