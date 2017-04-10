namespace Vp_himineu.Services.Application
{
    using System;
    using Vp_himineu.Services.Commands;
    using Vp_himineu.Services.Interfaces;

    public class Engine : IEngine
    {
        public void Runrunrunrunrun()
        {
            var exec = new Exec();
            while (true)
            {
                var commandLine = Console.ReadLine();
                if (commandLine == null)
                {
                    break;
                }

                commandLine = commandLine.Trim();
                if (string.IsNullOrEmpty(commandLine))
                {
                    continue;
                }

                try
                {
                    var comando = new Command(commandLine);
                    var commandResult = exec.TaskExecute(comando);
                    Console.WriteLine(commandResult);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}