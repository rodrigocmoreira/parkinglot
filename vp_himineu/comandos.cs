namespace Vp_himineu
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    using Vp_himineu.Contratos;
    using Vp_himineu.Domain.Entities;

    class exec
    {
        public class comando : IComando
        {
            public string Nome { get; set; }

            public IDictionary<string, string> Parametros { get; set; }

            public comando(string str)
            {
                this.Nome = str.Substring(0, str.IndexOf(' '));
                this.Parametros =
                    new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(
                        str.Substring(str.IndexOf(' ') + 1));
            }
        }

        v VehiclePark { get; set; }

        public string execução(IComando c)
        {
            if (c.Nome != "SetupPark" && this.VehiclePark == null) return "The vehicle park has not been set up";
            switch (c.Nome)
            {
                case "SetupPark":
                    //This doesnot work!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    // I donot know why!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    //VehiclePark=new VehiclePark(c.Parameters["sectors"]+1,c.Parameters["placesPerSector"]);
                    return "Vehicle park created";
                case "Рark":
                    switch (c.Parametros["type"])
                    {
                        case "car":
                            return this.VehiclePark.InsertCar(
                                new Carro(),
                                int.Parse(c.Parametros["sector"]),
                                int.Parse(c.Parametros["place"]),
                                DateTime.Parse(
                                    c.Parametros["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind)); //why round trip kind??
                        case "motorbike":
                            return this.VehiclePark.InsertMotorbike(
                                new Moto(),
                                int.Parse(c.Parametros["sector"]),
                                int.Parse(c.Parametros["place"]),
                                DateTime.Parse(
                                    c.Parametros["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind)); //stack overflow says this
                        case "truck":
                            return this.VehiclePark.InsertTruck(
                                new Caminhão(),
                                int.Parse(c.Parametros["sector"]),
                                int.Parse(c.Parametros["place"]),
                                DateTime.Parse(
                                    c.Parametros["time"],
                                    null,
                                    System.Globalization.DateTimeStyles.RoundtripKind)); //I wanna know
                    }

                    break;

                case "Exit":
                    return this.VehiclePark.ExitVehicle(
                        c.Parametros["licensePlate"],
                        DateTime.Parse(c.Parametros["time"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                        decimal.Parse(c.Parametros["money"]));
                case "Status": return this.VehiclePark.GetStatus();
                case "FindVehicle": return this.VehiclePark.FindVehicle(c.Parametros["licensePlate"]);
                case "VehiclesByOwner": return this.VehiclePark.FindVehiclesByOwner(c.Parametros["owner"]);
                default: throw new IndexOutOfRangeException("Invalid command.");
            }

            return "";
        }
    }
}















