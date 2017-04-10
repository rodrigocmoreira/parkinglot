namespace Vp_himineu.Services.Validations
{
    using System;
    using System.Text.RegularExpressions;

    using Vp_himineu.Domain.Entities;

    public class VehicleValidation
    {
        public void Validate(Vehicle vehicle)
        {
            if (!Regex.IsMatch(vehicle.LicensePlate, @"^[A-Z]{1,2}\d{4}[A-Z]{2}$"))
            {
                throw new ArgumentException("The license plate number is invalid.");
            }

            if (string.IsNullOrEmpty(vehicle.Owner))
            {
                throw new InvalidCastException("The owner is required.");
            }

            if (vehicle.RegularRate < 0)
            {
                throw new ArgumentException("The regular rate must be non-negative.");
            }

            if (vehicle.OvertimeRate < 0)
            {
                throw new ArgumentException("The overtime rate must be non-negative.");
            }

            if (vehicle.ReservedHours <= 0)
            {
                throw new ArgumentException("The reserved hours must be positive.");
            }
        }
    }
}
