using BikeDeals;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest_BikeDeals
{
    public class UnitTest_AddRentalEntry
    {
        public Deposit DepositFactory(int nrOfBikes, DateTime startDate, DateTime endDate, int nrOfRentalEntriesPerBike)
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, List<RentalEntry>> rentalEntries = new Dictionary<int, List<RentalEntry>>();

            Deposit testDeposit = new Deposit(rentalEntries, bikeList);

            for (int i = 0; i < nrOfBikes; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                List<RentalEntry> rentalEntry = new List<RentalEntry>();

                if (nrOfRentalEntriesPerBike > 0)
                {
                    for (int j = 0; j < nrOfRentalEntriesPerBike; j++)
                    {
                        rentalEntry.Add(new RentalEntry(startDate, endDate, bike));
                    }
                    rentalEntries.Add(i, rentalEntry);
                }
            }

            return (testDeposit);
        }

        [Fact]
        public void GetNextAvailableBike_NoBike_ShouldBeAvailable()
        {
            Deposit testDeposit = DepositFactory(0, DateTime.Today, DateTime.Today, 2);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)) == null, "no bike should be available");
        }

        [Fact]
        public void GetNextAvailableBike_OneBike_ShouldBeAvailable()
        {
            Deposit testDeposit = DepositFactory(1, DateTime.Today, DateTime.Today, 1);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5)) != null, "one bike is available");
        }

        [Fact]
        public void GetNextAvailableBike_ShouldReturn_BikeId()
        {
            Deposit testDeposit = DepositFactory(1, DateTime.Today, DateTime.Today, 1);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)).GetBikeId() == testDeposit.GetBikeList()[0].GetBikeId(), "verify bike id");
        }

        [Fact]
        public void GetNextAvailableBike_NoNewBike_ShouldBeAvailable()
        {
            Deposit testDeposit = DepositFactory(1, DateTime.Today.AddDays(1), DateTime.Today.AddDays(5), 1);

            Assert.True(testDeposit.GetNextAvailableBike(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)) == null, "no new bike should be available");
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForFiveRequired()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today, DateTime.Today.AddDays(5), 0);

            Assert.True(testDeposit.Rent(5, DateTime.Today, DateTime.Today.AddDays(5)) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTenRequired()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today, DateTime.Today.AddDays(5), 0);

            Assert.True(testDeposit.Rent(10, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTwoRequired()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today, DateTime.Today.AddDays(5), 0);

            Assert.True(testDeposit.Rent(2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)) == 2);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_ForTwoRequired()
        {
            Deposit testDeposit = DepositFactory(1, DateTime.Today, DateTime.Today.AddDays(5), 1);

            Assert.True(testDeposit.Rent(2, DateTime.Today, DateTime.Today.AddDays(2)) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfSomeBikesAreRented_ForNoneRequired()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 2);

            Assert.True(testDeposit.Rent(0, DateTime.Today, DateTime.Today) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_And_NoBikeIsRequired()
        {
            //List<Bike> bikeList = new List<Bike>();
            //Dictionary<int, List<RentalEntry>> rentedList = new Dictionary<int, List<RentalEntry>>();

            //Deposit testDeposit = new Deposit(rentedList, bikeList);

            Deposit testDeposit = DepositFactory(5, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 5);

            Assert.True(testDeposit.Rent(0, DateTime.Today, DateTime.Today.AddDays(1)) == 0);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_True_NoRentedBike()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today, DateTime.Today, 0);

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
            Deposit testDeposit = DepositFactory(5, DateTime.Today, DateTime.Today.AddDays(5), 1);

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == 5);
        }

        [Fact]
        public void GetNrOfDaysForOneRentalEntryPerBike_ShouldReturn_False_NegFiveDays()
        {
            Deposit testDeposit = DepositFactory(5, DateTime.Today.AddDays(5), DateTime.Today, 1);

            Assert.True(testDeposit.GetNrOfDaysForOneRentalEntryPerBike(0, 0) == -1);
        }
    }
}