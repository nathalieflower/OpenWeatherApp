using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class EndToEndTests
    {
        public const string apiKey = "f6ad1287a07ddeb1afbde1fe0b99db05";

        HttpClient Client = new HttpClient(); 

        [Fact]
        public async Task GetWeatherForLondon()
        {
            //arrange
            var baseUri = $"https://api.openweathermap.org/data/2.5/weather?q=London&APPID={apiKey}";

            //act
            var httpResponse = await Client.GetAsync(baseUri);

            //assert

            var statusCode = httpResponse.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var result = await httpResponse.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<dynamic>(result);
            Assert.Contains("weather", jsonResult);
        }
    }
}
