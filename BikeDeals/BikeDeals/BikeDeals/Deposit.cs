using System;
using System.Collections.Generic;

namespace BikeDeals
{
    public class Deposit
    {
        private List<Bike> bicycleList;
        private Dictionary<int, RentalEntry> rentalEntries;

        public Deposit(Dictionary<int, RentalEntry> rentalEntries, List<Bike> bicycleList)
        {
            this.rentalEntries = rentalEntries;
            this.bicycleList = bicycleList;
        }

        private void AddRentalEntry(int bikeId, RentalEntry entry)
        {
            this.rentalEntries.Add(bikeId, entry);
        }

        public Bike GetNextAvailableBike()
        {
            for (int i = 0; i < this.bicycleList.Count; i++)
            {
                if (!this.rentalEntries.ContainsKey(this.bicycleList[i].GetBikeId()))
                    return (this.bicycleList[i]);
            }
            return (null);
        }

        private List<Bike> GetListOfBikesToRent(int nrOfRquiredBikes)
        {
            List<Bike> bikesToRent = new List<Bike>();

            for (int i = 0; i < bicycleList.Count; i++)
            {
                if (!this.rentalEntries.ContainsKey(this.bicycleList[i].GetBikeId()) && bikesToRent.Count < nrOfRquiredBikes)
                    bikesToRent.Add(this.bicycleList[i]);
            }
            return (bikesToRent);
        }

        public int Rent(int nrOfRquiredBikes)
        {
            List<Bike> rentedBikes = GetListOfBikesToRent(nrOfRquiredBikes);

            for (int i = 0; i < rentedBikes.Count; i++)
            {
                this.AddRentalEntry(rentedBikes[i].GetBikeId(), new RentalEntry(DateTime.Now, DateTime.Now, rentedBikes[i]));
            }
            return (rentedBikes.Count);
        }

        public int GetNrOfDaysForARentedBike(int bikeId)
        {
            if (this.rentalEntries.ContainsKey(bikeId))
            {                
                RentalEntry rentalEntry = null;
                this.rentalEntries.TryGetValue(bikeId, out rentalEntry);
                if (rentalEntry != null)
                {
                    return (rentalEntry.GetNrOfRentDays());
                }
            }
            return (-1);
        }

        static void Main()
        {

        }
    }
}
