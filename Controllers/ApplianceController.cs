using DataTest.Services;
using Microsoft.AspNetCore.Mvc;
using DataTest.Models;
using DataTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTest.Data;

namespace DataTest.Controllers
{
    public class ApplianceController : Controller
    {
        private readonly ApplianceCostCalculator _applianceCostCalculator;
        private readonly ElectricityPriceService _electricityPriceService;

        public ApplianceController(ApplianceCostCalculator applianceCostCalculator, ElectricityPriceService electricityPriceService)
        {
            _applianceCostCalculator = applianceCostCalculator;
            _electricityPriceService = electricityPriceService;
        }

        public async Task<IActionResult> CalculateCost()
        {
            var model = new ApplianceCostViewModel
            {
                AvailableAppliances = GetAvailableAppliances(),
                ElectricityPrices = await _electricityPriceService.GetHourlyElectricityPricesAsync("SE3")
            };

            if (model.ElectricityPrices == null || !model.ElectricityPrices.Any())
            {
                Console.WriteLine("ElectricityPrices is empty or null");
            }
            else
            {
                Console.WriteLine($"ElectricityPrices has {model.ElectricityPrices.Count} entries.");
            }

            return View(model);
        }

        // POST: Appliance/CalculateCost
        [HttpPost]
        public async Task<IActionResult> CalculateCost(ApplianceCostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var electricityPrices = await _electricityPriceService.GetHourlyElectricityPricesAsync("SE3");
                model.ElectricityPrices = electricityPrices;

                var selectedAppliance = GetAvailableAppliances().FirstOrDefault(a => a.Name == model.SelectedAppliance);
                if (selectedAppliance != null)
                {
                    // Beräkna kostnaden med dynamiska elpriser
                    model.TotalCost = _applianceCostCalculator.CalculateApplianceCost(
                        model.StartTime,
                        model.Hours,
                        model.Minutes,
                        selectedAppliance.PowerInWatts,
                        electricityPrices
                    );

                    // Lägg till den nya aktiviteten
                    var newActivity = new ApplianceCostViewModel.Activity
                    {
                        ApplianceName = selectedAppliance.Name,
                        Hours = model.Hours,
                        Minutes = model.Minutes,
                        StartTime = model.StartTime,
                        Cost = model.TotalCost.Value
                    };

                    // Hämta aktiviteter från sessionen och lägg till den nya aktiviteten
                    var activities = HttpContext.Session.GetObjectFromJson<List<ApplianceCostViewModel.Activity>>("Activities") ?? new List<ApplianceCostViewModel.Activity>();
                    activities.Add(newActivity);

                    // Spara aktiviteterna i sessionen
                    HttpContext.Session.SetObjectAsJson("Activities", activities);

                    // Uppdatera modellens aktiviteter
                    model.Activities = activities;

                    model.AvailableAppliances = GetAvailableAppliances();
                }
                else
                {
                    // Om sessionen är tom, skapa en tom lista
                    model.Activities = HttpContext.Session.GetObjectFromJson<List<ApplianceCostViewModel.Activity>>("Activities") ?? new List<ApplianceCostViewModel.Activity>();
                }

               
            }

            return View(model);
        }

        private List<Appliance> GetAvailableAppliances()
        {
            return new List<Appliance>
            {
                new Appliance { Name = "Dishwasher", PowerInWatts = 1200, ImageUrl = "/images/DishWasher.jpg" },
                new Appliance { Name = "Washing Machine", PowerInWatts = 1000, ImageUrl = "/images/WashingMachine.jpg"},
                new Appliance { Name = "Dryer", PowerInWatts = 3000, ImageUrl = "/images/Dryer.jpg" },
                new Appliance { Name = "Vacuum", PowerInWatts = 1200, ImageUrl = "/images/Vacuum.jpg" }
            };
        }
    }
}
