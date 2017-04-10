namespace Vp_himineu.Domain.Entities
{
    public class Layout
    {
        public Layout(int numberOfSectors, int placesPerSector)
        {
            this.Sectors = numberOfSectors;
            this.PlacesSec = placesPerSector;
        }

        public int Sectors { get; set; }

        public int PlacesSec { get; set; }
    }
}