namespace Vp_himineu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Vp_himineu.Contratos;
    using Vp_himineu.Domain.Entities;
    using Vp_himineu.Domain.Interfaces;
    using Wintellect.PowerCollections;

    public class v : IVehiclePark
    {
        public layout layout;

        public DATA DATA;

        public v(int numberOfSectors, int placesPerSector)
        {
            this.layout = new layout(numberOfSectors, placesPerSector);
            this.DATA = new DATA(numberOfSectors);
        }

        public string InsertCar(Carro carro, int s, int p, DateTime t)
        {
            if (s > this.layout.sectors) return string.Format("There is no sector {0} in the park", s);
            if (p > this.layout.places_sec) return string.Format("There is no place {0} in sector {1}", p, s);
            if (this.DATA.park.ContainsKey(string.Format("({0},{1})", s, p)))
                return string.Format("The place ({0},{1}) is occupied", s, p);
            if (this.DATA.números.ContainsKey(carro.LicensePlate))
                return string.Format(
                    "There is already a vehicle with license plate {0} in the park",
                    carro.LicensePlate);
            this.DATA.carros_inpark[carro] = string.Format("({0},{1})", s, p);
            ;
            this.DATA.park[string.Format("({0},{1})", s, p)] = carro;
            this.DATA.números[carro.LicensePlate] = carro;
            this.DATA.d[carro] = t;
            this.DATA.ow[carro.Owner].Add(carro);
            this.DATA.count[s - 1]--;
            return string.Format("{0} parked successfully at place ({1},{2})", carro.GetType().Name, s, p);
        }

        public string InsertMotorbike(Moto moto, int s, int p, DateTime t)
        {
            if (s > this.layout.sectors) return string.Format("There is no sector {0} in the park", s);
            if (p > this.layout.places_sec) return string.Format("There is no place {0} in sector {1}", p, s);
            if (this.DATA.park.ContainsKey(string.Format("({0},{1})", s, p)))
                return string.Format("The place ({0},{1}) is occupied", s, p);
            if (this.DATA.números.ContainsKey(moto.LicensePlate))
                return string.Format(
                    "There is already a vehicle with license plate {0} in the park",
                    moto.LicensePlate);
            this.DATA.carros_inpark[moto] = string.Format("({0},{1})", s, p);
            this.DATA.park[string.Format("({0},{1})", s, p)] = moto;
            this.DATA.números[moto.LicensePlate] = moto;
            this.DATA.d[moto] = t;
            this.DATA.ow[moto.Owner].Add(moto);
            this.DATA.count[s - 1]++;
            return string.Format("{0} parked successfully at place ({1},{2})", moto.GetType().Name, s, p);
        }

        public string InsertTruck(Caminhão caminhão, int s, int p, DateTime t)
        {
            if (s > this.layout.sectors) return string.Format("There is no sector {0} in the park", s);
            if (p > this.layout.places_sec) return string.Format("There is no place {0} in sector {1}", p, s);
            if (this.DATA.park.ContainsKey(string.Format("({0},{1})", s, p)))
                return string.Format("The place ({0},{1}) is occupied", s, p);
            if (this.DATA.números.ContainsKey(caminhão.LicensePlate))
                return string.Format(
                    "There is already a vehicle with license plate {0} in the park",
                    caminhão.LicensePlate);
            this.DATA.carros_inpark[caminhão] = string.Format("({0},{1})", s, p);
            this.DATA.park[string.Format("({0},{1})", s, p)] = caminhão;
            this.DATA.números[caminhão.LicensePlate] = caminhão;
            this.DATA.d[caminhão] = t;
            this.DATA.ow[caminhão.Owner].Add(caminhão);
            return string.Format("{0} parked successfully at place ({1},{2})", caminhão.GetType().Name, s, p);
        }

        public string ExitVehicle(string l_pl, DateTime end, decimal money)
        {
            var vehicle = (this.DATA.números.ContainsKey(l_pl)) ? this.DATA.números[l_pl] : null;
            if (vehicle == null) return string.Format("There is no vehicle with license plate {0} in the park", l_pl);

            var start = this.DATA.d[vehicle];
            int endd = (int)Math.Round((end - start).TotalHours);
            var ticket = new StringBuilder();
            ticket.AppendLine(new string('*', 20))
                .AppendFormat("{0}", vehicle.ToString())
                .AppendLine()
                .AppendFormat("at place {0}", this.DATA.carros_inpark[vehicle])
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", (vehicle.ReservedHours * vehicle.RegularRate))
                .AppendLine()
                .AppendFormat(
                    "Overtime rate: ${0:F2}",
                    (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat(
                    "Total: ${0:F2}",
                    (vehicle.ReservedHours * vehicle.RegularRate
                     + (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)))
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", money)
                .AppendLine()
                .AppendFormat(
                    "Change: ${0:F2}",
                    money - ((vehicle.ReservedHours * vehicle.RegularRate)
                             + (endd > vehicle.ReservedHours
                                    ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate
                                    : 0)))
                .AppendLine()
                .Append(new string('*', 20));
            //DELETE
            int sector = int.Parse(
                this.DATA.carros_inpark[vehicle]
                    .Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            this.DATA.park.Remove(this.DATA.carros_inpark[vehicle]);
            this.DATA.carros_inpark.Remove(vehicle);
            this.DATA.números.Remove(vehicle.LicensePlate);
            this.DATA.d.Remove(vehicle);
            this.DATA.ow.Remove(vehicle.Owner, vehicle);
            this.DATA.count[sector - 1]--;
            //END OF DELETE
            return ticket.ToString();
        }

        public string GetStatus()
        {
            var places = this.DATA.count.Select(
                (sssss, iiiii) => string.Format(
                    "Sector {0}: {1} / {2} ({2}% full)",
                    iiiii + 1,
                    sssss,
                    this.layout.places_sec,
                    Math.Round((double)sssss / this.layout.places_sec * 100)));

            return string.Join(Environment.NewLine, places);
        }

        public string FindVehicle(string l_pl)
        {
            var vehicle = (this.DATA.números.ContainsKey(l_pl)) ? this.DATA.números[l_pl] : null;
            if (vehicle == null) return string.Format("There is no vehicle with license plate {0} in the park", l_pl);
            else return this.Input(new[] { vehicle });
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (!this.DATA.park.Values.Where(v => v.Owner == owner).Any())
                return string.Format("No vehicles by {0}", owner);
            else
            {
                var found = this.DATA.park.Values.ToList();
                var res = found;
                foreach (var f in found)
                {
                    res = res.Where(v => v.Owner == owner).ToList();
                }
                return string.Join(Environment.NewLine, this.Input(res));
            }
        }

        private string Input(IEnumerable<IVehicle> carros)
        {
            return string.Join(
                Environment.NewLine,
                carros.Select(
                    vehicle => string.Format(
                        "{0}{1}Parked at {2}",
                        vehicle.ToString(),
                        Environment.NewLine,
                        this.DATA.carros_inpark[vehicle])));
        }
    }

    public class DATA
    {
        public DATA(int numberOfSectors)
        {
            this.carros_inpark = new Dictionary<IVehicle, string>();
            this.park = new Dictionary<string, IVehicle>();
            this.números = new Dictionary<string, IVehicle>();
            this.d = new Dictionary<IVehicle, DateTime>();
            this.ow = new MultiDictionary<string, IVehicle>(false);
            this.count = new int[numberOfSectors];
        }

        #region Hard Stuff! My boss wrote that

        public Dictionary<IVehicle, string> carros_inpark { get; set; }

        public Dictionary<string, IVehicle> park { get; set; }

        public Dictionary<string, IVehicle> números { get; set; }

        public Dictionary<IVehicle, DateTime> d { get; set; }

        public MultiDictionary<string, IVehicle> ow { get; set; }

        public int[] count { get; set; }

        #endregion
    }

    public class layout
    {
        public int sectors;

        public int places_sec;

        public layout(int numberOfSectors, int placesPerSector)
        {
            if (numberOfSectors <= 0) throw new DivideByZeroException("The number of sectors must be positive.");
            this.sectors = numberOfSectors;
            if (placesPerSector <= 0) throw new DivideByZeroException("The number of places per sector must be positive.");
            this.places_sec = placesPerSector;
        }
    }
}