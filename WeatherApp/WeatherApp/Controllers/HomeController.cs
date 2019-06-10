using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = await new WeatherDetailsRetriever().GetWeather("London");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(WeatherDetails details)
        {
            var model = await new WeatherDetailsRetriever().GetWeather(details.name);

            if(model.Status != HttpStatusCode.OK)
                ModelState.AddModelError("city", "You have entered an invalid city please try again.");

            return View(model);
        }

    }
}