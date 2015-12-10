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
    }
}
