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
