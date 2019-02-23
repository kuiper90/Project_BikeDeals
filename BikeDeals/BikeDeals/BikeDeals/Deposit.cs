using System;
using System.Collections.Generic;

namespace BikeDeals
{
    public class Deposit
    {
        private List<Bike> bicycleList;        
        private Dictionary<int, List<RentalEntry>> rentalEntries;

        public Deposit(Dictionary<int, List<RentalEntry>> rentalEntries, List<Bike> bicycleList)
        {
            this.rentalEntries = rentalEntries;
            this.bicycleList = bicycleList;
        }

        private void AddRentalEntry(int bikeId, RentalEntry entry)
        {
            this.rentalEntries.TryGetValue(bikeId, out List<RentalEntry> bikeRentalEntries);

            if (bikeRentalEntries == null)
                bikeRentalEntries = new List<RentalEntry>();
            bikeRentalEntries.Add(entry);
            this.rentalEntries.Add(bikeId, bikeRentalEntries);
        }

        private bool RentalEntryOverlap(RentalEntry entry, DateTime startDate, DateTime endDate)
        {
            return ((entry.GetStartDate() <= endDate) && (startDate <= entry.GetEndDate()));
        }

        public bool RentalEntriesOverlap(int bicycleId, DateTime startDate, DateTime endDate)
        {
            List<RentalEntry> bikeRentalEntries;
            this.rentalEntries.TryGetValue(bicycleId, out bikeRentalEntries);
            foreach (RentalEntry entry in bikeRentalEntries)
            {
                if (RentalEntryOverlap(entry, startDate, endDate))
                    return (true);
            }
            return (false);
        }

        public Bike GetNextAvailableBike(DateTime startDate, DateTime endDate)
        {
            for (int i = 0; i < this.bicycleList.Count; i++)
            {
                int bicycleId = this.bicycleList[i].GetBikeId();
                if ((!this.rentalEntries.ContainsKey(bicycleId)) || (!RentalEntriesOverlap(bicycleId, startDate, endDate)))
                    return (this.bicycleList[i]);
            }
            return (null);
        }

        public int Rent(int nrOfRquiredBikes, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            Bike bicycle;

            for (int i = 0; i < bicycleList.Count; i++)
            {
                if (count < nrOfRquiredBikes)
                {
                    if ((bicycle = GetNextAvailableBike(startDate, endDate)) != null)
                    {
                        this.AddRentalEntry(bicycle.GetBikeId(), new RentalEntry(startDate, endDate, bicycle));
                        count++;
                    }
                }
            }
            return (count);
        }

        public int GetNrOfDaysForOneRentalEntryPerBike(int bikeId, int rentalEntryNumber)
        {
            if (this.rentalEntries.ContainsKey(bikeId))
            {                
                List<RentalEntry> rentalEntry = null;
                this.rentalEntries.TryGetValue(bikeId, out rentalEntry);
                if ((rentalEntry != null) && (rentalEntry.Count > 0) && (rentalEntry.Count >= rentalEntryNumber))
                {
                    int nrOfDays = rentalEntry[rentalEntryNumber].GetNrOfRentDays();
                    return (nrOfDays >= 0 ? nrOfDays : -1);
                }
            }
            return (-1);
        }

        static void Main()
        {

        }
    }
}
