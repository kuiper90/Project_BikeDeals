using System;
using System.Collections.Generic;

namespace BikeDeals
{
    public class RentalEntry
    {
        private DateTime startDate; 
        private DateTime endDate;
        private Bike rentedBike;

        public RentalEntry(DateTime startDate, DateTime endDate, Bike bike)
        {
            this.startDate = startDate;
            this.endDate = endDate;           
            this.rentedBike = bike;
        }

        public int GetNrOfRentDays()
        {
            return (endDate.Subtract(startDate).Days);
        }

        public DateTime GetStartDate()
        {
            return (this.startDate);
        }

        public DateTime GetEndDate()
        {
            return (this.endDate);
        }
    }
}
