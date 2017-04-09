namespace Vp_himineu
{
    using System.Globalization;
    using System.Threading;

    public static class Vp
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var engine = new Mecanismo();
            engine.Runrunrunrunrun();
        }
    }
}