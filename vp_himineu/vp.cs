namespace Vp_himineu
{
    using System.Globalization;
    using System.Threading;
    using Vp_himineu.Services.Application;

    public static class Vp
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var engine = new Engine();
            engine.Runrunrunrunrun();
        }
    }
}