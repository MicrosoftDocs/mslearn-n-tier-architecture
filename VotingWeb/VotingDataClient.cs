using System.Net.Http;
using System.Collections.Generic;
using VotingWeb.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VotingWeb
{
    public class VotingDataClient
    {
        private HttpClient HttpClient { get; set; }

        public VotingDataClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        internal async Task<IList<Counts>> GetCounts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"/api/VoteData");
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IList<Counts>>(await response.Content.ReadAsStringAsync());
        }

        internal async Task<HttpResponseMessage> AddVote(string candidate)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"/api/VoteData/{candidate}");
            return await HttpClient.SendAsync(request);
        }

        internal async Task DeleteCandidate(string candidate)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"/api/VoteData/{candidate}");
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}