using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WasteMonitorAPI.Database;
using WasteMonitorAPI.Hubs;
using WasteMonitorAPI.Services;

namespace WasteMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteDataController : ControllerBase
    {
        private WasteDataService _wasteDataService;
        private IHubContext<WasteMonitorHub> _wasteMonitorHub;

        public WasteDataController(WasteDataService wasteDataService, IHubContext<WasteMonitorHub> hubContext)
        {
            _wasteDataService = wasteDataService;
            _wasteMonitorHub = hubContext;
        }


        // GET: api/WasteData
        [HttpGet]
        public IEnumerable<WasteData> Get()
        {
            return _wasteDataService.GetAllData();
        }
        [HttpGet]
        [Route("latest")]
        public IActionResult Latest(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = _wasteDataService.GetLatest();

            if (data == null)
            {
                return NotFound(new { message = "Data not found" });
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("summary")]
        public ActionResult<IEnumerable<double>> Summary()
        {
            try
            {
                var result = _wasteDataService.GetWeightSummary();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        [Route("histogram")]
        public ActionResult<Dictionary<DayOfWeek, int>> Histogram()
        {
            try
            {
                var result = _wasteDataService.GetDayHistogram();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // GET: api/WasteData/Latest
        [HttpGet]
        [Route("refresh")]
        public IActionResult Refresh()
        {
            _wasteMonitorHub.Clients.All.SendAsync("Refresh");
            return Ok();
        }

        // POST: api/WasteData
        [HttpPost]
        public IActionResult Post([FromQuery]WasteData wasteData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int id = _wasteDataService.InsertData(wasteData);
                return CreatedAtAction("Post", new { Id = id });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Couldn't add data." });
            }
        }


    }
}
