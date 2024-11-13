using DataTest.Models;

namespace DataTest.ViewModel
{
    public class ApplianceCostViewModel
    {
        public List<Appliance> AvailableAppliances { get; set; }
        public string SelectedAppliance { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public int Hours { get; set; }
        public double? TotalCost { get; set; }
    }

}
