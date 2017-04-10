namespace Vp_himineu.Services.Interfaces
{
    using System;
    using Vp_himineu.Domain.Entities;

    /// <summary>
    /// Interface that supply the main tasks the vehicle park mut provide
    /// </summary>
    public interface IVehiclePark
    {
        /// <summary>
        /// Insert a car in the vehicle park
        /// </summary>
        /// <param name="car"> A car object </param>
        /// <param name="sector"> The sector to park </param>
        /// <param name="placeNumber"> the park place number </param>
        /// <param name="startTime"> the parking start time </param>
        /// <returns>Return the type of vehicle parked and the park site position"</returns>
        string InsertCar(Car car, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Insert a motorbike in the vehicle park
        /// </summary>
        /// <param name="motorbike"> A Motorbike object </param>
        /// <param name="sector"> The sector to park </param>
        /// <param name="placeNumber"> the park place number </param>
        /// <param name="startTime"> the parking start time </param>
        /// <returns> Return the type of vehicle parked and the park site position </returns>
        string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Insert a truck in the vehicle park
        /// </summary>
        /// <param name="truck"> A Motorbike object </param>
        /// <param name="sector"> The sector to park </param>
        /// <param name="placeNumber"> the park place number </param>
        /// <param name="startTime"> the parking start time </param>
        /// <returns> Return the type of vehicle parked and the park site position </returns>
        string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Register the exit of a vehicle
        /// </summary>
        /// <param name="licensePlate"> The license plate of the vehicle </param>
        /// <param name="endTime"> The time of exit of the vehicle </param>
        /// <param name="amountPaid"> The total amount paid by the costumer </param>
        /// <returns> returns a string extract with info about this vehicle stay at the parking</returns>
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);

        /// <summary>
        /// Get parking status summary
        /// </summary>
        /// <returns> A summary of current parking situation </returns>
        string GetStatus();

        /// <summary>
        /// Find a vehicle by its licence plate
        /// </summary>
        /// <param name="licensePlate"> The license plate of the vehicle </param>
        /// <returns> return search result has string </returns>
        string FindVehicle(string licensePlate);

        /// <summary>
        /// Find a vehicle by it's ownew name
        /// </summary>
        /// <param name="owner"> Owner of the vehicle</param>
        /// <returns> return search result of the vehicle has string </returns>
        string FindVehiclesByOwner(string owner);
    }
}
