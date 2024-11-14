using DataTest.Models;

namespace DataTest.Data
{
    public class DataCollection
    {
        public List<OneHouseHoldDataOneWeek> OneHouseHold24HrsCollection { get; set; } = new List<OneHouseHoldDataOneWeek>();

        // Lägg till funktioner här för att hantera olika tidsperioder
        public List<OneHouseHoldDataOneWeek> FilterByTimePeriod(DateTime start, DateTime end)
        {
            return OneHouseHold24HrsCollection.Where(d => d.AtDateTime >= start && d.AtDateTime <= end).ToList();
        }
    }
}
