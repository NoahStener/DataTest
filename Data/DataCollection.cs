namespace DataTest.Data
{
    public class DataCollection
    {
        public List<OneHouseHoldData24hrs> OneHouseHold24HrsCollection { get; set; } = new List<OneHouseHoldData24hrs>();

        // Lägg till funktioner här för att hantera olika tidsperioder
        public List<OneHouseHoldData24hrs> FilterByTimePeriod(DateTime start, DateTime end)
        {
            return OneHouseHold24HrsCollection.Where(d => d.AtDateTime >= start && d.AtDateTime <= end).ToList();
        }
    }
}
