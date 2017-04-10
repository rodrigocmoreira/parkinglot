namespace Vp_himineu.Data
{
    using System;
    using System.Collections.Generic;
    using Vp_himineu.Domain.Interfaces;
    using Wintellect.PowerCollections;

    public class Repository
    {
        public Repository(int numberOfSectors)
        {
            this.CarsInPark = new Dictionary<IVehicle, string>();
            this.Park = new Dictionary<string, IVehicle>();
            this.Numbers = new Dictionary<string, IVehicle>();
            this.DateParkDictionary = new Dictionary<IVehicle, DateTime>();
            this.OwnersMultiDictionary = new MultiDictionary<string, IVehicle>(false);
            this.Count = new int[numberOfSectors];
        }

        public Dictionary<IVehicle, string> CarsInPark { get; set; }

        public Dictionary<string, IVehicle> Park { get; set; }

        public Dictionary<string, IVehicle> Numbers { get; set; }

        public Dictionary<IVehicle, DateTime> DateParkDictionary { get; set; }

        public MultiDictionary<string, IVehicle> OwnersMultiDictionary { get; set; }

        public int[] Count { get; set; }
    }
}
