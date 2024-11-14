using DataTest.Data;
using DataTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataTest.Controllers
{
    public class EnergyController : Controller
    {
        private readonly EnergyAnalysisService _energyAnalysisService;
        private readonly DataProcessor _dataProcessor;

        public EnergyController(EnergyAnalysisService energyAnalysisService, DataProcessor dataProcessor)
        {
            _energyAnalysisService = energyAnalysisService;
            _dataProcessor = dataProcessor;
        }

        public ActionResult Index() 
        {
            var dailyConsumption = _dataProcessor.GetDailyEnergyConsumption();
            return View(dailyConsumption);
        }

    }
}
