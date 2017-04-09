namespace Vp_himineu.Domain.Entities
{
    public class Moto : Vehicle
    {
        public Moto()
        {
            this.RegularRate = (decimal)1.35;
            this.OvertimeRate = 3M;
        }
    }
}
