using DataTest.Data;
using DataTest.Models;
using DataTest.Services;
using DataTest.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EnergyAnalysisService _energyAnalysisService;

        public HomeController(ILogger<HomeController> logger, EnergyAnalysisService energyAnalysisService)
        {
            _logger = logger;
            _energyAnalysisService = energyAnalysisService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
