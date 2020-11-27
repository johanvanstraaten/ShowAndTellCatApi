using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using TheCallOfCats.Models;

namespace TheCallOfCats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //IRestRequest restRequest = new RestRequest("/v1/breeds",Method.GET);

            //restRequest.AddQueryParameter("attach_breed", "0");

            //var response = Meow(restRequest);
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private DisplayModel Meow(IRestRequest restRequest)
        //{
        //    RestClient restClient = new RestClient("https://api.thecatapi.com");

        //    restRequest.AddHeader("x-api-key", "3a21310d-ff0d-49f0-9c77-1cff25a3da71");

        //    var response = restClient.Execute(restRequest);

        //    DisplayModel displayModel = new DisplayModel();

        //    displayModel.Response = response;
        //    displayModel.Url = restClient.BuildUri(restRequest).ToString();

        //    return displayModel;
        //}
    }
}
