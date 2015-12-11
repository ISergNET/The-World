using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace The_World.Services
{
    public class CoordService
    {
        private readonly ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string location)
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Message = "Unsuccessful query result"
            };
            var bingMapsKey = Startup.Configuration["AppSettings:BingKey"];
            var encodeName = WebUtility.UrlEncode(location);
            var strUrl =
                $"http://dev.virtualearth.net/REST/v1/Locations?q={encodeName}&key={bingMapsKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(strUrl);
            // Read out the results
            // Fragile, might need to change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
