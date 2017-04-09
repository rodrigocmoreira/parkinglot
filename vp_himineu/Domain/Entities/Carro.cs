namespace Vp_himineu.Domain.Entities
{
    using Vp_himineu.Domain.Interfaces;

    public class Carro : Vehicle
    {
        public Carro()
        {
            this.RegularRate = 2;
            this.OvertimeRate = 3.5M;
        }
    }
}
