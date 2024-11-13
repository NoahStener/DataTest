namespace DataTest.Services
{

    public class ApplianceCostCalculator
    {
        // Price in SEK per kWh during peak and off-peak hours
        private readonly double peakHourPricePerKwh = 2.5;
        private readonly double offPeakHourPricePerKwh = 1.2;

        // Method to calculate the cost of running an appliance for a certain period
        public double CalculateApplianceCost(DateTime startTime, int hours, double appliancePowerInWatts)
        {
            double totalCost = 0.0;
            DateTime currentTime = startTime;

            for (int i = 0; i < hours; i++)
            {
                double pricePerKwh = CalculateEnergyCost(currentTime);
                double hourlyEnergyConsumptionInKwh = appliancePowerInWatts / 1000.0; // Convert watts to kWh
                double hourlyCost = hourlyEnergyConsumptionInKwh * pricePerKwh; // Cost per hour based on kWh
                totalCost += hourlyCost;

                // Increase the time by one hour
                currentTime = currentTime.AddHours(1);
            }

            return totalCost;
        }

        // Method to calculate the energy cost based on time
        public double CalculateEnergyCost(DateTime dateTime)
        {
            return IsPeakHours(dateTime) ? peakHourPricePerKwh : offPeakHourPricePerKwh;
        }

        // Helper method to determine if a time is during peak hours
        public bool IsPeakHours(DateTime dateTime)
        {
            var hour = dateTime.Hour;
            return hour >= 6 && hour < 22;
        }
    }


}
