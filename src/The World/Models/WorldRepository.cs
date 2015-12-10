using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace The_World.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips
                .OrderBy(x => x.Name)
                .ToList();
        }

        public IEnumerable<Trip> GetAllTripsWthStops()
        {
            return _context.Trips
                .Include(x => x.Stops)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
