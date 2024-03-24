using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StadiumAnalytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController(ISensorDbContext context) : ControllerBase
    {

        private readonly ILogger<SensorDataController> _logger;

        private readonly ISensorDbContext _context = context;

        /// <summary>
        /// HTTP API to get total number of people entered or exited from the stadium for a given date time range 
        /// </summary>
        /// <param name="startTimeStamp">start time stamp</param>
        /// <param name="endTimeStamp">end time stamp</param>
        /// <param name="gateName">Name of the Gate</param>
        /// <param name="eventType">Entry or Exit</param>
        /// <returns>Json object with Total entered or exit from the given gate</returns>

        [HttpGet]
        public IActionResult GetSensorData(string startTimeStamp, string endTimeStamp, string gateName, string eventType)
        {
            var query = _context.SensorData.AsQueryable();
            DateTime startExactTime = DateTime.ParseExact(startTimeStamp, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endExactTime = DateTime.ParseExact(endTimeStamp, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);


            if (!string.IsNullOrEmpty(gateName))
                query = query.Where(d => d.GateName == gateName);

            if (!string.IsNullOrEmpty(eventType))
                query = query.Where(d => d.EventType == eventType);

            query = query.Where(d => d.TimeStamp >= startExactTime && d.TimeStamp <= endExactTime);

            var result = query.Sum(d => d.NumOfPeople);
            return Ok(new { 
                gate = gateName, type= eventType, TotalEntered = result 
            });  ;
        }

    }
}
