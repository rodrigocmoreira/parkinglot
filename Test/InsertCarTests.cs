namespace Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vp_himineu.Domain.Entities;
    using Vp_himineu.Services.Application;
    using Vp_himineu.Services.Commands;

    [TestClass]
    public class InsertCarTests
    {
        [TestMethod]
        public void InsertCarWithSuccess()
        {
            // arrange
            var vehiclePark = new VehiclePark(3, 5);
            var command = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 1, \"place\""
                + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var car = new Car
                          {
                              LicensePlate = command.Parameters["licensePlate"],
                              Owner = command.Parameters["owner"],
                              ReservedHours = int.Parse(command.Parameters["hours"])
                          };

            // act
            var result = vehiclePark.InsertCar(
                car,
                int.Parse(command.Parameters["sector"]),
                int.Parse(command.Parameters["place"]),
                DateTime.Parse(
                    command.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            // assert
            Assert.AreEqual("Car parked successfully at place (1,5)", result, "Car inserted correctly");
        }

        [TestMethod]
        public void InsertCarWithErrorAtPlace()
        {
            // arrange
            var vehiclePark = new VehiclePark(3, 5);
            var commandOne = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 1, \"place\""
                                      + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var carOne = new Car
                          {
                              LicensePlate = commandOne.Parameters["licensePlate"],
                              Owner = commandOne.Parameters["owner"],
                              ReservedHours = int.Parse(commandOne.Parameters["hours"])
                          };

            var commandTwo = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 1, \"place\""
                                      + ": 5, \"licensePlate\": \"CA1101HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var carTwo = new Car
                             {
                                 LicensePlate = commandTwo.Parameters["licensePlate"],
                                 Owner = commandTwo.Parameters["owner"],
                                 ReservedHours = int.Parse(commandTwo.Parameters["hours"])
                             };

            // act
            var resultOne = vehiclePark.InsertCar(
                carOne,
                int.Parse(commandOne.Parameters["sector"]),
                int.Parse(commandOne.Parameters["place"]),
                DateTime.Parse(
                    commandOne.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            var resultTwo = vehiclePark.InsertCar(
                carTwo,
                int.Parse(commandTwo.Parameters["sector"]),
                int.Parse(commandTwo.Parameters["place"]),
                DateTime.Parse(
                    commandTwo.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            // assert
            Assert.AreEqual("Car parked successfully at place (1,5)", resultOne, "Car inserted correctly");
            Assert.AreEqual("The place (1,5) is occupied", resultTwo, "Place occupied");
        }

        [TestMethod]
        public void InsertCarWithErrorAtCar()
        {
            // arrange
            var vehiclePark = new VehiclePark(3, 5);
            var commandOne = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 1, \"place\""
                                         + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var carOne = new Car
                             {
                                 LicensePlate = commandOne.Parameters["licensePlate"],
                                 Owner = commandOne.Parameters["owner"],
                                 ReservedHours = int.Parse(commandOne.Parameters["hours"])
                             };

            var commandTwo = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 2, \"place\""
                                         + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var carTwo = new Car
                             {
                                 LicensePlate = commandTwo.Parameters["licensePlate"],
                                 Owner = commandTwo.Parameters["owner"],
                                 ReservedHours = int.Parse(commandTwo.Parameters["hours"])
                             };

            // act
            var resultOne = vehiclePark.InsertCar(
                carOne,
                int.Parse(commandOne.Parameters["sector"]),
                int.Parse(commandOne.Parameters["place"]),
                DateTime.Parse(
                    commandOne.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            var resultTwo = vehiclePark.InsertCar(
                carTwo,
                int.Parse(commandTwo.Parameters["sector"]),
                int.Parse(commandTwo.Parameters["place"]),
                DateTime.Parse(
                    commandTwo.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            // assert
            Assert.AreEqual("Car parked successfully at place (1,5)", resultOne, "Car inserted correctly");
            Assert.AreEqual("There is already a vehicle with license plate CA1001HH in the park", resultTwo, "Car already parked");
        }

        [TestMethod]
        public void InsertCarWithErrorInSector()
        {
            // arrange
            var vehiclePark = new VehiclePark(1, 1);
            var command = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 2, \"place\""
                                      + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var car = new Car
                          {
                              LicensePlate = command.Parameters["licensePlate"],
                              Owner = command.Parameters["owner"],
                              ReservedHours = int.Parse(command.Parameters["hours"])
                          };

            // act
            var result = vehiclePark.InsertCar(
                car,
                int.Parse(command.Parameters["sector"]),
                int.Parse(command.Parameters["place"]),
                DateTime.Parse(
                    command.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            // assert
            Assert.AreEqual("There is no sector 2 in the park", result, "Sector dont exist");
        }

        [TestMethod]
        public void InsertCarWithErrorInPlace()
        {
            // arrange
            var vehiclePark = new VehiclePark(1, 1);
            var command = new Command("Park {\"type\": \"car\", \"time\": \"2015 - 05 - 04T10: 30:00.0000000\", \"sector\": 1, \"place\""
                                      + ": 5, \"licensePlate\": \"CA1001HH\", \"owner\": \"Jay Margareta\", \"hours\": 1}");

            var car = new Car
            {
                LicensePlate = command.Parameters["licensePlate"],
                Owner = command.Parameters["owner"],
                ReservedHours = int.Parse(command.Parameters["hours"])
            };

            // act
            var result = vehiclePark.InsertCar(
                car,
                int.Parse(command.Parameters["sector"]),
                int.Parse(command.Parameters["place"]),
                DateTime.Parse(
                    command.Parameters["time"],
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind));

            // assert
            Assert.AreEqual("There is no place 5 in sector 1", result, "Place dont exist");
        }
    }
}