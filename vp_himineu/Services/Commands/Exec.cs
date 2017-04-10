namespace Vp_himineu.Services.Commands
{
    using System;
    using Vp_himineu.Domain.Entities;
    using Vp_himineu.Services.Application;
    using Vp_himineu.Services.Interfaces;
    using Vp_himineu.Services.Validations;

    public class Exec
    {
        private VehiclePark VehiclePark { get; set; }

        public string TaskExecute(ICommand command)
        {
            if (command.Name != "SetupPark" && this.VehiclePark == null)
            {
                return "The vehicle park has not been set up";
            }

            // TODO this switch can be replaced by strategy pattern in the future
            switch (command.Name)
            {
                case "SetupPark":
                    this.VehiclePark = new VehiclePark(Convert.ToInt32(command.Parameters["sectors"]), Convert.ToInt32(command.Parameters["placesPerSector"]));
                    return "Vehicle park created";
                case "Park":
                    var vehicleValidation = new VehicleValidation();
                    switch (command.Parameters["type"])
                    {
                        case "car":
                            var car = new Car
                                    {
                                        LicensePlate = command.Parameters["licensePlate"],
                                        Owner = command.Parameters["owner"],
                                        ReservedHours = int.Parse(command.Parameters["hours"])
                                    };
                            vehicleValidation.Validate(car);
                            return this.VehiclePark.InsertCar(
                                car,
                                int.Parse(command.Parameters["sector"]),
                                int.Parse(command.Parameters["place"]),
                                DateTime.Parse(
                                    command.Parameters["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind));
                        case "motorbike":
                            var motorbike = new Motorbike
                                            {
                                                LicensePlate = command.Parameters["licensePlate"],
                                                Owner = command.Parameters["owner"],
                                                ReservedHours = int.Parse(command.Parameters["hours"])
                                            };
                            vehicleValidation.Validate(motorbike);
                            return this.VehiclePark.InsertMotorbike(
                                motorbike,
                                int.Parse(command.Parameters["sector"]),
                                int.Parse(command.Parameters["place"]),
                                DateTime.Parse(
                                    command.Parameters["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind));
                        case "truck":
                            var truck = new Truck()
                                            {
                                                LicensePlate = command.Parameters["licensePlate"],
                                                Owner = command.Parameters["owner"],
                                                ReservedHours = int.Parse(command.Parameters["hours"])
                                            };
                            vehicleValidation.Validate(truck);
                            return this.VehiclePark.InsertTruck(
                                truck,
                                int.Parse(command.Parameters["sector"]),
                                int.Parse(command.Parameters["place"]),
                                DateTime.Parse(
                                    command.Parameters["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind));
                        default:
                            return "Invalid parameter.";
                    }

                case "Exit":
                    return this.VehiclePark.ExitVehicle(
                        command.Parameters["licensePlate"],
                        DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                        decimal.Parse(command.Parameters["paid"]));

                case "Status":
                    return this.VehiclePark.GetStatus();

                case "FindVehicle":
                    return this.VehiclePark.FindVehicle(command.Parameters["licensePlate"]);

                case "VehiclesByOwner":
                    return this.VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]);

                default:
                    return "Invalid command.";
            }
        }
    }
}