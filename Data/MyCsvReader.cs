using CsvHelper;
using System.Globalization;

namespace DataTest.Data
{
    public class MyCsvReader
    {
       
        private string file24Hours = "C:\\Users\\Deral\\Desktop\\Varberg Energi\\OneHousehold_24Hours.csv";
        private string file1Week = "C:\\Users\\Deral\\Desktop\\Varberg Energi\\OneHousehold_OneWeek.csv";
        private string file10Households = "C:\\Users\\Deral\\Desktop\\Varberg Energi\\TenHouseholds_OneWeek.csv";
        public List<OneHouseHoldData24hrs> ReadDataFromOneHouseHoldTwentyFourHours()
        {
            using var reader = new StreamReader(file24Hours);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<OneHouseHoldData24hrs>().ToList();
        }

        // Metod för att läsa veckodata för flera hushåll
        public List<OneHouseHoldDataOneWeek> ReadDataFromOneHouseHoldOneWeek()
        {
            using var reader = new StreamReader(file1Week);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<OneHouseHoldDataOneWeek>().ToList();
        }

        // Metod för att läsa dagsdata för ett hushåll
        public List<TenHouseHoldsOneWeekData> ReadDataFromTenHouseHoldsOneWeek()
        {
            using var reader = new StreamReader(file10Households);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TenHouseHoldsOneWeekData>().ToList();
        }
    }
}
