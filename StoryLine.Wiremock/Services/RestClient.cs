using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StoryLine.Exceptions;

namespace StoryLine.Wiremock.Services
{
    public class RestClient : IRestClient
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public void PostJson(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsync(url, new StringContent(string.Empty, Encoding.UTF8, "application/json")).Result;
                    if (!result.IsSuccessStatusCode)
                        throw new ExpectationException($"Request to \"{url}\" resulted and error. Response code was: {(int)result.StatusCode} ({result.StatusCode}).");

                    result.Content.ReadAsByteArrayAsync().Wait();
                }
                catch (Exception ex)
                {
                    throw new ExpectationException($"Failed to send request to \"{url}\".", ex);
                }
            }
        }

        public TResult PostJson<TResult>(string url, object body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Argument is null or whitespace", nameof(url));

            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsync(url, new StringContent(GetJson(body), Encoding.UTF8, "application/json")).Result;
                    if (!result.IsSuccessStatusCode)
                        throw new ExpectationException($"Request to \"{url}\" resulted and error. Response code was: {(int) result.StatusCode} ({result.StatusCode}).");

                    return GetObject<TResult>(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception ex)
                {
                    throw new ExpectationException($"Failed to send request to \"{url}\".", ex);
                }
            }
        }

        public void Delete(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.DeleteAsync(url).Result;
                    if (!result.IsSuccessStatusCode)
                        throw new ExpectationException($"Request to \"{url}\" resulted and error. Response code was: {(int)result.StatusCode} ({result.StatusCode}).");

                    result.Content.ReadAsByteArrayAsync().Wait();
                }
                catch (Exception ex)
                {
                    throw new ExpectationException($"Failed to send request to \"{url}\".", ex);
                }
            }
        }

        private static T GetObject<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
                return default(T);

            return JsonConvert.DeserializeObject<T>(content, Settings);
        }

        private static string GetJson(object body)
        {
            if (body == null)
                return string.Empty;

            return JsonConvert.SerializeObject(body, Settings);
        }


    }
}