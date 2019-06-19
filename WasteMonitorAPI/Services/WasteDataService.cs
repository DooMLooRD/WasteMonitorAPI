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

        public List<(int, double)> GetWeightSummary()
        {
            var result = _wasteMonitorContext.WasteData.Where(c => c.wasEmptied).GroupBy(c => c.DateTime.Month).Select(c => new { key = c.Key, value = c.Sum(d => d.Weight) }).ToList();
            int diff = result.Count - 10;
            return result.Select(c => (c.key, c.value)).Skip(diff > 0 ? diff : 0).ToList();
        }

        public Dictionary<DayOfWeek, int> GetDayHistogram()
        {
            Dictionary<DayOfWeek, int> result = new Dictionary<DayOfWeek, int>();
            for (int i = 0; i < 7; i++)
            {
                result[(DayOfWeek)i] = 0;
            }
            int currentMonth = DateTime.Now.Month - 1;
            currentMonth = currentMonth > 0 ? currentMonth : 12;
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
