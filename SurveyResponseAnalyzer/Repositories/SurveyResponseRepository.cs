using Newtonsoft.Json;
using SurveyResponseAnalyzer.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SurveyResponseAnalyzer.Repositories
{
    /// <summary>
    /// Basic repo for loading the responses
    /// </summary>
    public class SurveyResponseRepository
    {

        private readonly string _baseUrl;
        public SurveyResponseRepository(string url)
        {
            _baseUrl = url;
        }

        /// <summary>
        /// Get survey responses
        /// </summary>
        /// <returns>List of responses</returns>
        public async Task<IEnumerable<SurveyResponse>> GetResponses()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);
                response.EnsureSuccessStatusCode(); // Throw if not success

                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<SurveyResponse>>(stringResponse);
            }
        }
    }
}
