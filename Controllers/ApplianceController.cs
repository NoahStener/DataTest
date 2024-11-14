using DataTest.Services;
using Microsoft.AspNetCore.Mvc;
using global::DataTest.Data;
using global::DataTest.Models;
using global::DataTest.ViewModel;
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
            // Hämta tidigare aktiviteter från session
            var activities = HttpContext.Session.GetObjectFromJson<List<ApplianceCostViewModel.Activity>>("Activities") ?? new List<ApplianceCostViewModel.Activity>();

            var selectedAppliance = GetAvailableAppliances().Find(a => a.Name == model.SelectedAppliance);
            if (selectedAppliance != null)
            {
                // Beräkna den totala varaktigheten i timmar och minuter
                double totalHours = model.Hours + (model.Minutes / 60.0);

                // Beräkna kostnaden baserat på både timmar och minuter
                model.TotalCost = _applianceCostCalculator.CalculateApplianceCost(
                    model.StartTime,
                    (int)Math.Floor(totalHours), // Hela timmar
                    selectedAppliance.PowerInWatts
                );

                // Lägg till kostnaden för de återstående minuterna om sådana finns
                double remainingMinutesCost = (selectedAppliance.PowerInWatts / 1000) * (model.Minutes % 60) / 60.0 *
                                              (model.StartTime.Hour >= 6 && model.StartTime.Hour < 22 ? 2.5 : 1.2);
                model.TotalCost += remainingMinutesCost;

                // Lägg till den nya aktiviteten till Activities-listan med både timmar och minuter
                activities.Add(new ApplianceCostViewModel.Activity
                {
                    ApplianceName = selectedAppliance.Name,
                    Hours = model.Hours,
                    Minutes = model.Minutes,
                    StartTime = model.StartTime,
                    Cost = model.TotalCost.Value
                });

                // Uppdatera sessionen med den nya aktivitetslistan
                HttpContext.Session.SetObjectAsJson("Activities", activities);
            }

            model.AvailableAppliances = GetAvailableAppliances();
            model.Activities = activities;

            return View(model);
        }


        private List<Appliance> GetAvailableAppliances()
        {
            return new List<Appliance>
                {
                new Appliance { Name = "Dishwasher", PowerInWatts = 1200, ImageUrl = "/images/DishWasher.jpg" },
                new Appliance { Name = "Washing Machine", PowerInWatts = 500, ImageUrl = "/images/WashingMachine.jpg"},
                new Appliance { Name = "Dryer", PowerInWatts = 3000, ImageUrl = "/images/Dryer.jpg" },
                new Appliance { Name = "Vacuum", PowerInWatts = 1200, ImageUrl = "/images/Vacuum.jpg" }

                };
        }
    }
}


