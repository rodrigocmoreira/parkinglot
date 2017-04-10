namespace Vp_himineu.Services.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Vp_himineu.Data;
    using Vp_himineu.Domain.Entities;
    using Vp_himineu.Domain.Interfaces;
    using Vp_himineu.Services.Interfaces;

    public class VehiclePark : IVehiclePark
    {
        private readonly Layout layout;
        private readonly Repository repository;

        public VehiclePark(int numberOfSectors, int placesPerSector)
        {
            if (numberOfSectors <= 0)
            {
                throw new DivideByZeroException("The number of Sectors must be positive.");
            }

            if (placesPerSector <= 0)
            {
                throw new DivideByZeroException("The number of places per sector must be positive.");
            }

            this.layout = new Layout(numberOfSectors, placesPerSector);
            this.repository = new Repository(numberOfSectors);
        }

        public string InsertCar(Car car, int sector, int place, DateTime time)
        {
            if (sector > this.layout.Sectors)
            {
                return $"There is no sector {sector} in the park";
            }

            if (place > this.layout.PlacesSec)
            {
                return $"There is no place {place} in sector {sector}";
            }

            if (this.repository.Park.ContainsKey($"({sector},{place})"))
            {
                return $"The place ({sector},{place}) is occupied";
            }

            if (this.repository.Numbers.ContainsKey(car.LicensePlate))
            {
                return $"There is already a vehicle with license plate {car.LicensePlate} in the park";
            }

            this.repository.CarsInPark[car] = $"({sector},{place})";
            this.repository.Park[$"({sector},{place})"] = car;
            this.repository.Numbers[car.LicensePlate] = car;
            this.repository.DateParkDictionary[car] = time;
            this.repository.OwnersMultiDictionary[car.Owner].Add(car);
            this.repository.Count[sector - 1]++;
            return $"{car.GetType().Name} parked successfully at place ({sector},{place})";
        }

        public string InsertMotorbike(Motorbike motorbike, int sector, int place, DateTime time)
        {
            if (sector > this.layout.Sectors)
            {
                return $"There is no sector {sector} in the park";
            }

            if (place > this.layout.PlacesSec)
            {
                return $"There is no place {place} in sector {sector}";
            }

            if (this.repository.Park.ContainsKey($"({sector},{place})"))
            {
                return $"The place ({sector},{place}) is occupied";
            }

            if (this.repository.Numbers.ContainsKey(motorbike.LicensePlate))
            {
                return $"There is already a vehicle with license plate {motorbike.LicensePlate} in the park";
            }

            this.repository.CarsInPark[motorbike] = $"({sector},{place})";
            this.repository.Park[$"({sector},{place})"] = motorbike;
            this.repository.Numbers[motorbike.LicensePlate] = motorbike;
            this.repository.DateParkDictionary[motorbike] = time;
            this.repository.OwnersMultiDictionary[motorbike.Owner].Add(motorbike);
            this.repository.Count[sector - 1]++;
            return $"{motorbike.GetType().Name} parked successfully at place ({sector},{place})";
        }

        public string InsertTruck(Truck truck, int sector, int place, DateTime time)
        {
            if (sector > this.layout.Sectors)
            {
                return $"There is no sector {sector} in the park";
            }

            if (place > this.layout.PlacesSec)
            {
                return $"There is no place {place} in sector {sector}";
            }

            if (this.repository.Park.ContainsKey($"({sector},{place})"))
            {
                return $"The place ({sector},{place}) is occupied";
            }

            if (this.repository.Numbers.ContainsKey(truck.LicensePlate))
            {
                return $"There is already a vehicle with license plate {truck.LicensePlate} in the park";
            }

            this.repository.CarsInPark[truck] = $"({sector},{place})";
            this.repository.Park[$"({sector},{place})"] = truck;
            this.repository.Numbers[truck.LicensePlate] = truck;
            this.repository.DateParkDictionary[truck] = time;
            this.repository.OwnersMultiDictionary[truck.Owner].Add(truck);
            this.repository.Count[sector - 1]++;
            return $"{truck.GetType().Name} parked successfully at place ({sector},{place})";
        }

        public string ExitVehicle(string licencePlate, DateTime end, decimal money)
        {
            var vehicle = this.repository.Numbers.ContainsKey(licencePlate) ? this.repository.Numbers[licencePlate] : null;
            if (vehicle == null)
            {
                return $"There is no vehicle with license plate {licencePlate} in the park";
            }

            var start = this.repository.DateParkDictionary[vehicle];
            var endd = (int)Math.Round((end - start).TotalHours);
            var ticket = new StringBuilder();
            ticket.AppendLine(new string('*', 20))
                .AppendFormat("{0}", vehicle)
                .AppendLine()
                .AppendFormat("at place {0}", this.repository.CarsInPark[vehicle])
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", vehicle.ReservedHours * vehicle.RegularRate)
                .AppendLine()
                .AppendFormat("Overtime rate: ${0:F2}", endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat(
                    "Total: ${0:F2}",
                    (vehicle.ReservedHours * vehicle.RegularRate) + (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", money)
                .AppendLine()
                .AppendFormat(
                    "Change: ${0:F2}",
                    money - ((vehicle.ReservedHours * vehicle.RegularRate) + (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)))
                .AppendLine()
                .Append(new string('*', 20));

            // DELETE
            var sector = int.Parse(this.repository.CarsInPark[vehicle].Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            this.repository.Park.Remove(this.repository.CarsInPark[vehicle]);
            this.repository.CarsInPark.Remove(vehicle);
            this.repository.Numbers.Remove(vehicle.LicensePlate);
            this.repository.DateParkDictionary.Remove(vehicle);
            this.repository.OwnersMultiDictionary.Remove(vehicle.Owner, vehicle);
            this.repository.Count[sector - 1]--;

            // END OF DELETE
            return ticket.ToString();
        }

        public string GetStatus()
        {
            var places = this.repository.Count.Select(
                (sssss, iiiii) => $"Sector {iiiii + 1}: {sssss} / {this.layout.PlacesSec} ({Math.Round((double)sssss / this.layout.PlacesSec * 100)}% full)");

            return string.Join(Environment.NewLine, places);
        }

        public string FindVehicle(string licencePlate)
        {
            var vehicle = this.repository.Numbers.ContainsKey(licencePlate) ? this.repository.Numbers[licencePlate] : null;
            return vehicle == null ? $"There is no vehicle with license plate {licencePlate} in the park" : this.Input(new[] { vehicle });
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (this.repository.Park.Values.All(v => v.Owner != owner))
            {
                return $"No vehicles by {owner}";
            }

            var found = this.repository.Park.Values.ToList();
            var res = found;

            // PERFORMANCE: useless foreach
            res = res.Where(v => v.Owner == owner).ToList();

            return string.Join(Environment.NewLine, this.Input(res));
        }

        private string Input(IEnumerable<IVehicle> carros)
        {
            return string.Join(
                Environment.NewLine,
                carros.Select(
                    vehicle => $"{vehicle.ToString()}{Environment.NewLine}Parked at {this.repository.CarsInPark[vehicle]}"));
        }
    }
}
