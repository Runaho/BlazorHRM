using System;
using System.Text;
using System.Text.Json;

namespace BlazorHRM.Client.Services
{
    public class ServiceRequestHelper<T>
    {
        JsonSerializerOptions jsonSerializerOptions;
        public ServiceRequestHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public HttpClient _httpClient { get; }

        public async Task<T?> GetAsync(string path, string query = "")
        {
            string url = path;
            if (!string.IsNullOrEmpty(query))
                url += $"{query}";

            return await JsonSerializer.DeserializeAsync<T>
                 (await _httpClient.GetStreamAsync(url), options: jsonSerializerOptions);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path, string id)
        {
            return await _httpClient.DeleteAsync($"{path}?id={id}");
        }

        public async Task<T> SendAsync(string path, object model, RequestTypes requestTypes)
        {
            var json = new StringContent(JsonSerializer.Serialize(model), encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            switch (requestTypes)
            {
                case RequestTypes.Post:
                    response = await _httpClient.PostAsync(path, json);
                    break;
                case RequestTypes.Put:
                    response = await _httpClient.PutAsync(path, json);
                    break;
                default:
                    break;
            }
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), options: jsonSerializerOptions);
        }
    }



    public enum RequestTypes
    {
        Put,
        Post
    }
}

