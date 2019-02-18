using BikeDeals;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest_BikeDeals
{
    public class UnitTest_AddRentalEntries
    {
        [Fact]
        public void GetNextAvailableBike_NoBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Assert.True(testDeposit.GetNextAvailableBike() == null, "no bike should be available");
        }

        [Fact]
        public void GetNextAvailableBike_OneBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(1);
            bikeList.Add(bike);

            Assert.True(testDeposit.GetNextAvailableBike() != null, "one bike is available");
        }

        [Fact]
        public void GetNextAvailableBike_ShouldReturn_BikeId()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(1);
            bikeList.Add(bike);

            Assert.True(testDeposit.GetNextAvailableBike().GetBikeId() == bike.GetBikeId(), "verify bike id");
        }

        [Fact]
        public void GetNextAvailableBike_NoNewBike_ShouldBeAvailable()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Bike bike = new Bike(1);
            bikeList.Add(bike);

            rentedList.Add(1, new RentalEntry(DateTime.Today, DateTime.Today, bike));
            Assert.True(testDeposit.GetNextAvailableBike() == null, "no new bike should be available");
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForFiveRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));

            //Assert.True(testDeposit.GetListOfBikesToRent(5).Count == 5); //if "GetListOfBikesToRent" method is public  
            Assert.True(testDeposit.Rent(5) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTenRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));

            //Assert.True(testDeposit.GetListOfBikesToRent(10).Count == 5); //if "GetListOfBikesToRent" method is public
            Assert.True(testDeposit.Rent(10) == 5);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfListCountFive_ForTwoRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));

            //Assert.True(testDeposit.GetListOfBikesToRent(2).Count == 2); //if "GetListOfBikesToRent" method is public
            Assert.True(testDeposit.Rent(2) == 2);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_ForTwoRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                rentedList.Add(i, new RentalEntry(DateTime.Today, DateTime.Today, bike));
            }

            //Assert.True(testDeposit.GetListOfBikesToRent(2).Count == 0); //if "GetListOfBikesToRent" method is public
            Assert.True(testDeposit.Rent(2) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_ForNoneRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
                bikeList.Add(new Bike(i));

            //Assert.True(testDeposit.GetListOfBikesToRent(0).Count == 0); //if "GetListOfBikesToRent" method is public
            Assert.True(testDeposit.Rent(0) == 0);
        }

        [Fact]
        public void Rent_ShouldReturn_True_IfAllBikesAreRented_And_NoBikeIsRequired()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Assert.True(testDeposit.Rent(0) == 0);
        }

        [Fact]
        public void GetnNrOfDaysForARentedBike_ShouldReturn_True_NoRentedBike()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            Assert.True(testDeposit.GetNrOfDaysForARentedBike(0) == -1);
        }

        [Fact]
        public void GetnNrOfDaysForARentedBike_ShouldReturn_True_ZeroDays()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                rentedList.Add(i, new RentalEntry(DateTime.Today, DateTime.Today, bike));
            }

            Assert.True(testDeposit.GetNrOfDaysForARentedBike(0) == 0);
        }

        [Fact]
        public void GetnNrOfDaysForARentedBike_ShouldReturn_True_FiveDays()
        {
            List<Bike> bikeList = new List<Bike>();
            Dictionary<int, RentalEntry> rentedList = new Dictionary<int, RentalEntry>();

            Deposit testDeposit = new Deposit(rentedList, bikeList);

            for (int i = 0; i < 5; i++)
            {
                Bike bike = new Bike(i);
                bikeList.Add(bike);
                rentedList.Add(i, new RentalEntry(DateTime.Today, DateTime.Today.AddDays(5), bike));
            }

            Assert.True(testDeposit.GetNrOfDaysForARentedBike(0) == 5);
        }
    }
}