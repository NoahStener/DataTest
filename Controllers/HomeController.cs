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
        private readonly DataManipulator _dataManipulator;
        private readonly EnergyAnalysisService _energyAnalysisService;

        public HomeController(ILogger<HomeController> logger, DataManipulator dataManipulator, EnergyAnalysisService energyAnalysisService)
        {
            _logger = logger;
            _dataManipulator = dataManipulator;
            _energyAnalysisService = energyAnalysisService;
        }

        public IActionResult Index()
        {
            var data = _dataManipulator.GetDataForOneHousehold24Hours();
            var totalEnergy = _dataManipulator.GetTotalEnergyForOneHousehold24Hours();

            var viewModel = new HomeViewModel
            {
                HouseHoldData = data,
                TotalEnergyConsumption = totalEnergy
            };
            return View(viewModel);
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
