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
        private readonly DataProcessor _dataProcessor;
        private readonly ElectricityPriceService _electricityPriceService;

        public HomeController(DataProcessor dataProcessor, ElectricityPriceService electricityPriceService)
        {
            _dataProcessor = dataProcessor;
            _electricityPriceService = electricityPriceService;
        }

        public async Task<IActionResult> Index()
        {
            // Hämta energiförbrukning för att visa på startsidan
            var dailyConsumption = _dataProcessor.GetDailyEnergyConsumption();

            // Hämta aktuellt elpris
            var currentPrices = await _electricityPriceService.GetHourlyElectricityPricesAsync("SE3");
            var currentPrice = currentPrices.FirstOrDefault()?.Price ?? 0;

            // Skicka datan till vyn
            var model = new HomeViewModel
            {
                DailyConsumption = dailyConsumption,
                CurrentElectricityPrice = currentPrice
            };

            return View(model);
        }
    }
}
