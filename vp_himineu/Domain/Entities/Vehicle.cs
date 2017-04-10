namespace Vp_himineu.Domain.Entities
{
    using System.Text;
    using Vp_himineu.Domain.Interfaces;

    public abstract class Vehicle : IVehicle
    {
        public string LicensePlate { get; set; }

        public string Owner { get; set; }

        public decimal RegularRate { get; set; }

        public decimal OvertimeRate { get; set; }

        public int ReservedHours { get; set; }

        public override string ToString()
        {
            var vehicle = new StringBuilder();
            vehicle.AppendFormat("{0} [{2}], owned by {1}", this.GetType().Name, this.LicensePlate, this.Owner);
            return vehicle.ToString();
        }
    }
}
