namespace Vp_himineu.Domain.Interfaces
{
    public interface IVehicle
    {
        string LicensePlate { get; set; }

        string Owner { get; set; }

        decimal RegularRate { get; set; }

        decimal OvertimeRate { get; set; }

        int ReservedHours { get; set; }
    }
}
