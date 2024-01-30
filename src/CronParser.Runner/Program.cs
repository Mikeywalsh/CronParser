using System;
using CronParser.Engine.Exceptions;
using CronParser.Engine.ResultProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace CronParser.Runner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = Startup.BuildServiceCollection().BuildServiceProvider();

            var cronResultOrchestrator = serviceProvider.GetService<ICronResultOrchestrator>();

            while (true)
            {
                try
                {
                    Console.WriteLine("Input q at any point to quit!");
                    Console.WriteLine("Please input your cron string in the format: * * * * * /command");

                    var input = Console.ReadLine() ?? string.Empty;

                    if (input == "q")
                        break;

                    Console.WriteLine(cronResultOrchestrator!.GetCronString(input));
                }
                catch (CronParseException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine();
            }
        }
    }
}