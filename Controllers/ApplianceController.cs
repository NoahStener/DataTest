using DataTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataTest.Controllers
{
    using global::DataTest.Models;
    using global::DataTest.ViewModel;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    namespace DataTest.Controllers
    {
        public class ApplianceController : Controller
        {
            private readonly ApplianceCostCalculator _applianceCostCalculator;

            public ApplianceController(ApplianceCostCalculator applianceCostCalculator)
            {
                _applianceCostCalculator = applianceCostCalculator;
            }

            public IActionResult CalculateCost()
            {
                var model = new ApplianceCostViewModel
                {
                    AvailableAppliances = GetAvailableAppliances()
                };
                return View(model);
            }

            [HttpPost]
            public IActionResult CalculateCost(ApplianceCostViewModel model)
            {
                var selectedAppliance = GetAvailableAppliances().Find(a => a.Name == model.SelectedAppliance);
                if (selectedAppliance != null)
                {
                    model.TotalCost = _applianceCostCalculator.CalculateApplianceCost(model.StartTime, model.Hours, selectedAppliance.PowerInWatts);
                }
                model.AvailableAppliances = GetAvailableAppliances();
                return View(model);
            }

            private List<Appliance> GetAvailableAppliances()
            {
                return new List<Appliance>
                {
                new Appliance { Name = "Dishwasher", PowerInWatts = 1200 },
                new Appliance { Name = "Washing Machine", PowerInWatts = 500 },
                new Appliance { Name = "Dryer", PowerInWatts = 3000 },
                new Appliance { Name = "Vacuum", PowerInWatts = 1200 }

                };
            }
        }
    }

}
