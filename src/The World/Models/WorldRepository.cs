using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace The_World.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<IWorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<IWorldRepository> logger)
        {
            _logger = logger;
            _context = context;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips
            .OrderBy(x => x.Name)
            .ToList();

            }
            catch (Exception exception)
            {
                _logger.LogError("Could not get Trips from Database.", exception);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWthStops()
        {
            try
            {
                return _context.Trips
            .Include(x => x.Stops)
            .OrderBy(x => x.Name)
            .ToList();

            }
            catch (Exception exception)
            {
                _logger.LogError("Could not get Trips from Database.", exception);
                return null;
            }
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public bool SaveAll() => _context.SaveChanges() > 0;

        public Trip GetTripByName(string tripName) => _context.Trips.Include(x => x.Stops).FirstOrDefault(x => x.Name == tripName);

        public void AddStop(string tripName, Stop newStop)
        {
            Trip trip = GetTripByName(tripName);
            if (trip == null) throw new NullReferenceException($"Couldn't find a trip {tripName}");
            try
            {
                newStop.Order = trip.Stops.Max(x => x.Order) + 1;

            }
            catch (Exception)
            {

                newStop.Order = 0;
            }
            trip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }
    }
}
