namespace Vp_himineu.Domain.Entities
{
    public class Truck : Vehicle
    {
        // TODO remove hard coded values
        public Truck()
        {
            this.RegularRate = (decimal)4.75f;
            this.OvertimeRate = 6.2M;
        }
    }
}
