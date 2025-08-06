using System;

namespace CarManager.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;
        private const string Make = "VW";
        private const string Model = "Golf";
        private const double FuelConsumption = 5.0;
        private const double FuelCapacity = 50.0;
        private const double FuelAmountIsZeroByDefault = 0.0;
        
        [Test]
        public void Constructor_ShouldInitializeCarCorrectly()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            
            Assert.AreEqual(car.Make, Make);
            Assert.AreEqual(car.Model, Model);
            Assert.AreEqual(car.FuelConsumption, FuelConsumption);
            Assert.AreEqual(car.FuelCapacity, FuelCapacity);
            Assert.AreEqual(car.FuelAmount, FuelAmountIsZeroByDefault);
        }

        [Test]
        public void Constructor_ShouldThrowException_WhenMakeIsNull()
        {
            string makeIsNull = null;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(makeIsNull, Model, FuelConsumption, FuelCapacity);
            }, "Make cannot be null or empty!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenMakeIsEmpty()
        {
            string makeIsEmpty = string.Empty;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(makeIsEmpty, Model, FuelConsumption, FuelCapacity);
            }, "Make cannot be null or empty!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenModelIsNull()
        {
            string modelIsNull = null;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, modelIsNull, FuelConsumption, FuelCapacity);
            }, "Model cannot be null or empty!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenModelIsEmpty()
        {
            string modelIsEmpty = string.Empty;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, modelIsEmpty, FuelConsumption, FuelCapacity);
            }, "Model cannot be null or empty!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenFuelConsumptionIsZero()
        {
            double fuelConsumptionIsZero = 0.0;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, Model, fuelConsumptionIsZero, FuelCapacity);
            }, "Fuel consumption cannot be zero or negative!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenFuelConsumptionIsNegative()
        {
            double fuelConsumptionIsNegative = -1.0;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, Model, fuelConsumptionIsNegative, FuelCapacity);
            }, "Fuel consumption cannot be zero or negative!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenFuelCapacityIsZero()
        {
            double fuelCapacityIsZero = 0.0;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, Model, FuelConsumption, fuelCapacityIsZero);
            }, "Fuel capacity cannot be zero or negative!");
        }
        
        [Test]
        public void Constructor_ShouldThrowException_WhenFuelCapacityIsNegative()
        {
            double fuelCapacityIsNegative = -1.0;
            
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(Make, Model, FuelConsumption, fuelCapacityIsNegative);
            }, "Fuel capacity cannot be zero or negative!");
        }
        
        [Test]
        public void Refuel_ShouldIncreaseFuelAmount_WhenValidAmountIsProvided()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = 20.0;
            
            car.Refuel(refuelAmount);
            
            Assert.AreEqual(car.FuelAmount, refuelAmount);
        }
        
        [Test]
        public void Refuel_ShouldIncreaseFuelAmountToMaxCapacity_WhenAmountIsMoreThenCapacity()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = 100.0;
            
            car.Refuel(refuelAmount);
            
            Assert.AreEqual(car.FuelAmount, FuelCapacity);
        }
        
        [Test]
        public void Refuel_ShouldThrowException_WhenFuelAmountIsNegative()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = -1.0;

            Assert.Throws<ArgumentException>(() => car.Refuel(refuelAmount), "Fuel amount cannot be zero or negative!");
        }
        
        [Test]
        public void Refuel_ShouldThrowException_WhenFuelAmountIsZero()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = 0.0;

            Assert.Throws<ArgumentException>(() => car.Refuel(refuelAmount), "Fuel amount cannot be zero or negative!");
        }
        
        [Test]
        public void Drive_ShouldDriveCarCorrectly_WhenEnoughFuelIsAvailable()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = 50.0;
            car.Refuel(refuelAmount);
            double distance = 100.0;
            
            car.Drive(distance);
            
            Assert.AreEqual(car.FuelAmount, refuelAmount - (distance / 100) * FuelConsumption);
        }

        [Test]
        public void Drive_ShouldThrowException_WhenNotEnoughFuelIsAvailable()
        {
            car = new Car(Make, Model, FuelConsumption, FuelCapacity);
            double refuelAmount = 10.0;
            car.Refuel(refuelAmount);
            double distance = 300.0;
            
            Assert.Throws<InvalidOperationException>(() => car.Drive(distance), "You don't have enough fuel to drive!");
        }
    }
}