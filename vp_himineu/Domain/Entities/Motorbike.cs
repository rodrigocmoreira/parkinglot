namespace Vp_himineu.Domain.Entities
{
    public class Motorbike : Vehicle
    {
        // TODO remove hard coded values
        public Motorbike()
        {
            this.RegularRate = (decimal)1.35;
            this.OvertimeRate = 3M;
        }
    }
}
