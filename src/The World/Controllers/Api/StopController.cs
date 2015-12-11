using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using The_World.Models;

namespace The_World.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<StopController> _logger;

        public StopController(IWorldRepository repository, ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName);
                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(x => x.Order)));

            }
            catch (Exception exception)
            {
                _logger.LogError($"Couldn't find trip {tripName}", exception);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json($"Failed to find trip {tripName}");
            }
        }

        [HttpPost("")]
        public JsonResult Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //map to the entity
                    var newStop = Mapper.Map<Stop>(vm);

                    //looking up geolocation

                    //save entity
                    _repository.AddStop(tripName, newStop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }

                }
                _logger.LogError($"Validation failed to save new stop for {tripName}");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Validation failed to save new stop for {tripName}");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to save new stop for {tripName}", exception);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Failed to save new stop for {tripName}");
            }
        }
    }
}
