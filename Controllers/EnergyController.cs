using DataTest.Data;
using DataTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataTest.Controllers
{
    public class EnergyController : Controller
    {
        private readonly EnergyAnalysisService _energyAnalysisService;
        private readonly DataManipulator dataManipulator;

        public EnergyController(EnergyAnalysisService energyAnalysisService, DataManipulator dataManipulator)
        {
            _dataManipulator = dataManipulator;
            _energyAnalysisService = energyAnalysisService;
        }

        private readonly DataManipulator _dataManipulator;

        //public IActionResult OneHousehold24Hours()
        //{
        //    var dataWithCost = _dataManipulator.GetDataWithCostForOneHousehold24Hours();
        //    double totalCost = _dataManipulator.GetTotalCostForOneHousehold24Hours();

        //    ViewData["TotalCost"] = totalCost;
        //    return View(dataWithCost); // Skicka listan med kostnad som modell till vyn
        //}
    }
}
