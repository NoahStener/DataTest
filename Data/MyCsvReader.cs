using CsvHelper;
using DataTest.Models;
using System.Globalization;

namespace DataTest.Data
{
    public class MyCsvReader
    {
      
        private string file1Week = "C:\\Användare\\Noah\\OneHousehold_OneWeek.csv";

        // Metod för att läsa veckodata för flera hushåll
        public List<OneHouseHoldDataOneWeek> ReadDataFromOneHouseHoldOneWeek()
        {
            using var reader = new StreamReader(file1Week);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<OneHouseHoldDataOneWeek>().ToList();
        }

    }
}
