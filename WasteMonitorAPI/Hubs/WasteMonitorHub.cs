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
        public void SendData(double weight, double fillingLevel)
        {

            _wasteDataService.InsertData(new WasteData()
            {
                DateTime = DateTime.Now,
                FillingLevel = fillingLevel,
                Weight = weight
            });
        }

    }
}
