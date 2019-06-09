using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        public const string apiKey = "f6ad1287a07ddeb1afbde1fe0b99db05";
        public async Task<ActionResult> Index(string search)
        {
            return View(await GetWeather(search));
        }

        public async Task<WeatherDetails> GetWeather(string search)
        {

            if (search == null)
            {
                search = "London";
            }
            WeatherDetails weatherDetails = null;
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={search}&units=metric&APPID={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    weatherDetails = JsonConvert.DeserializeObject<WeatherDetails>(jsonResponse);
                }

                else
                {
                    throw new Exception("Location Not Valid");
                }
                return weatherDetails;
            }
        }
    }
}