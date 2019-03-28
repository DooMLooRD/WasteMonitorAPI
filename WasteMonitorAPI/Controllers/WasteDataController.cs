using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasteMonitorAPI.Database;
using WasteMonitorAPI.Services;

namespace WasteMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteDataController : ControllerBase
    {
        private WasteDataService _wasteDataService;

        public WasteDataController(WasteDataService wasteDataService)
        {
            _wasteDataService = wasteDataService;
        }


        // GET: api/WasteData
        [HttpGet]
        public IEnumerable<WasteData> Get()
        {
            return _wasteDataService.GetAllData();
        }

        // GET: api/WasteData/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = _wasteDataService.GetData(id);

            if (data == null)
            {
                return NotFound(new { message = "Data not found" });
            }
            return Ok(data);
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

        // PUT: api/WasteData/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
