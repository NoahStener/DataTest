using DataTest.Models;
using System;
using System.Collections.Generic;

namespace DataTest.Services
{
    public class ApplianceCostCalculator
    {
        // Method to calculate the cost of running an appliance for a certain period
        public double CalculateApplianceCost(DateTime startTime, int hours, int minutes, double appliancePowerInWatts, List<ElectricityPrice> electricityPrices)
        {
            double totalCost = 0.0;
            DateTime currentTime = startTime;

            // Loopa igenom timmarna och hämta priset för varje timme från API-datan
            for (int i = 0; i < hours; i++)
            {
                var priceData = electricityPrices.Find(p => p.Time.Hour == currentTime.Hour);
                double pricePerKwh = priceData?.Price ?? 0.4; // Default-pris om ingen data finns
                double hourlyEnergyConsumptionInKwh = appliancePowerInWatts / 1000.0;
                totalCost += hourlyEnergyConsumptionInKwh * pricePerKwh;
                currentTime = currentTime.AddHours(1);
            }

            // Lägg till kostnad för eventuella kvarvarande minuter
            if (minutes > 0)
            {
                var minutePriceData = electricityPrices.Find(p => p.Time.Hour == currentTime.Hour);
                double minutePricePerKwh = minutePriceData?.Price ?? 0.4;
                double energyConsumptionPerMinuteInKwh = (appliancePowerInWatts / 1000.0) / 60.0;
                totalCost += energyConsumptionPerMinuteInKwh * minutes * minutePricePerKwh;
            }

            return totalCost;
        }
    }
}






