using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WasteMonitorAPI.Database;

namespace WasteMonitorAPI.Services
{
    public class WasteDataService
    {
        private WasteMonitorContext _wasteMonitorContext;

        public WasteDataService(WasteMonitorContext wasteMonitorContext)
        {
            _wasteMonitorContext = wasteMonitorContext;
        }

        public List<double> GetWeightSummary()
        {
            List<double> result = _wasteMonitorContext.WasteData.Where(c => c.wasEmptied).GroupBy(c => c.DateTime.Month).Select(c => c.Sum(d => d.Weight)).ToList();
            int diff = result.Count - 10;
            return result.Skip(diff > 0 ? diff : 0).ToList();
        }

        public Dictionary<DayOfWeek, int> GetDayHistogram()
        {
            Dictionary<DayOfWeek, int> result = new Dictionary<DayOfWeek, int>();
            for (int i = 0; i < 7; i++)
            {
                result[(DayOfWeek)i] = 0;
            }
            int currentMonth = DateTime.Now.Month - 1;
            List<WasteData> filteredData = _wasteMonitorContext.WasteData.Where(c => c.wasEmptied && c.DateTime.Month == currentMonth).ToList();
            foreach (var data in filteredData)
            {
                result[data.DateTime.DayOfWeek]++;
            }

            return result;

        }

        public List<WasteData> GetAllData()
        {
            return _wasteMonitorContext.WasteData.ToList();
        }

        public WasteData GetData(int id)
        {
            return _wasteMonitorContext.WasteData.FirstOrDefault(e => e.Id == id);
        }

        public int InsertData(WasteData data)
        {
            _wasteMonitorContext.WasteData.Add(data);
            _wasteMonitorContext.SaveChanges();
            return data.Id;
        }

        public WasteData GetLatest()
        {
            return _wasteMonitorContext.WasteData.Last();
        }
    }
}
