using System.Collections.Generic;

namespace The_World.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWthStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName);
        void AddStop(string tripName, Stop newStop);
    }
}