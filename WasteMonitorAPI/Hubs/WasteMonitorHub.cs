using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WasteMonitorAPI.Database;
using WasteMonitorAPI.Services;

namespace WasteMonitorAPI.Hubs
{
    public class WasteMonitorHub : Hub
    {
        private WasteDataService _wasteDataService;
        public WasteMonitorHub(WasteDataService wasteDataService)
        {
            _wasteDataService = wasteDataService;
        }
        public void SendData(DateTime dateTime, double weight, double fillingLevel, bool empty = false)
        {

            _wasteDataService.InsertData(new WasteData()
            {
                DateTime = dateTime,
                FillingLevel = fillingLevel,
                Weight = weight,
                wasEmptied = empty
            });
        }

    }
}
