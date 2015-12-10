﻿using System.Collections.Generic;

namespace The_World.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWthStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
    }
}