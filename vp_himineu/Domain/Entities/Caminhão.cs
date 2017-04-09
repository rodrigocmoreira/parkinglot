namespace Vp_himineu.Domain.Entities
{
    public class Caminhão : Vehicle
    {
        public Caminhão()
        {
            this.RegularRate = (decimal)((double)4.75f);
            this.OvertimeRate = 6.2M;
        }
    }
}
