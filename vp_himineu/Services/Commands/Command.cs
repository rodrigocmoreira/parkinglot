namespace Vp_himineu.Services.Commands
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using Vp_himineu.Services.Interfaces;

    public class Command : ICommand
    {
        public Command(string str)
        {
            this.Name = str.Substring(0, str.IndexOf(' '));

            // PERFOMANCE: Could be improved
            this.Parameters = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(str.Substring(str.IndexOf(' ') + 1));
        }

        public string Name { get; set; }

        public IDictionary<string, string> Parameters { get; set; }
    }
}
