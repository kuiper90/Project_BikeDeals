using System;
using System.Collections.Generic;
using System.Linq;
using static BikeDeals.Deposit;

namespace BikeDeals
{
    public class Bike
    {
        protected int bikeId;

        public Bike(int bikeId)
        {
            this.bikeId = bikeId;
        }

        public int GetBikeId()
        {
            return (this.bikeId);
        }
    }
}
