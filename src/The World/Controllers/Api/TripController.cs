using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using The_World.Models;

namespace The_World.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripController> _logger;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWthStops());
            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Put([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Attempt to save new Trip.");
                    Trip newTrip = Mapper.Map<Trip>(vm);

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<TripViewModel>(newTrip));
                }

            }
            catch (Exception exception)
            {
                _logger.LogError("Couldn't save new Trip.", exception);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new {Message = exception.Message});
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(false);
        }
    }
}
