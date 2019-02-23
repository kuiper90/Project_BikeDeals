using BikeDeals;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest_BikeDeals
{
    public class UnitTest_AddRentalEntry
    {
        [Fact]
        public void GetNextAvailableBike_NoBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)) == null, "no bike should be available");
        }

        [Fact]
        public void GetNextAvailableBike_OneBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(0);
            bikeList.Add(bike);

            List<RentalEntry> rentalEntry = new List<RentalEntry>();

            rentalEntry.Add(new RentalEntry(DateTime.Today, DateTime.Today, bike));
            rentedList.Add(0, rentalEntry);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5)) != null, "one bike is available");
        }

        [Fact]
        public void GetNextAvailableBike_ShouldReturn_BikeId()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 2; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                List<RentalEntry> rentalEntry = new List<RentalEntry>();

                rentalEntry.Add(new RentalEntry(DateTime.Today, DateTime.Today, bike));
                rentedList.Add(i, rentalEntry);
            }

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)).GetBikeId() == bikeList[0].GetBikeId(), "verify bike id");
        }

        [Fact]
        public void GetNextAvailableBike_NoNewBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(0);
            bikeList.Add(bike);

            List<RentalEntry> rentalEntry = new List<RentalEntry>();

            rentalEntry.Add(new RentalEntry(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), bike));            
            rentedList.Add(0, rentalEntry);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)) == null, "no new bike should be available");
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForFiveRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));
            
            Assert.True(testDeposit.Rent(5, DateTime.Today, DateTime.Today.AddDays(5)) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTenRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));
                        
            Assert.True(testDeposit.Rent(10, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTwoRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));
                        
            Assert.True(testDeposit.Rent(2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)) == 2);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_ForTwoRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                List<RentalEntry> rentalEntries = new List<RentalEntry>();

                rentalEntries.Add(new RentalEntry(DateTime.Today, DateTime.Today.AddDays(2), bike));
                rentedList.Add(i, rentalEntries);
            }
                        
            Assert.True(testDeposit.Rent(2, DateTime.Today, DateTime.Today.AddDays(2)) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_ForNoneRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));
                        
            Assert.True(testDeposit.Rent(0, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_And_NoBikeIsRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Assert.True(testDeposit.Rent(0, DateTime.Today, DateTime.Today.AddDays(1)) == 0);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_True_NoRentedBike()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(0);
            bikeList.Add(bike);
            List<RentalEntry> rentalEntry = new List<RentalEntry>();
            
            rentedList.Add(0, rentalEntry);

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == -1);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_True_ZeroDays()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(0);
            bikeList.Add(bike);
            List<RentalEntry> rentalEntry = new List<RentalEntry>();

            rentalEntry.Add(new RentalEntry(DateTime.Today, DateTime.Today, bike));
            rentalEntry.Add(new RentalEntry(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), bike));
            rentedList.Add(0, rentalEntry);

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == 0);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_True_FiveDays()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);

                List<RentalEntry> rentalEntry = new List<RentalEntry>();
                
                rentalEntry.Add(new RentalEntry(DateTime.Today, DateTime.Today.AddDays(5), bike));
                rentedList.Add(i, rentalEntry);
            }

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == 5);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_False_NegFiveDays()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(0);
            bikeList.Add(bike);

            List<RentalEntry> rentalEntry = new List<RentalEntry>();

            rentalEntry.Add(new RentalEntry(DateTime.Today.AddDays(5), DateTime.Today, bike));
            rentedList.Add(0, rentalEntry);

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == -1);
        }
    }
}