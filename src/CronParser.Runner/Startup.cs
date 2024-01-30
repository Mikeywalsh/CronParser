using CronParser.Engine.Formatting;
using CronParser.Engine.Parsing;
using CronParser.Engine.Parsing.SubParsers;
using CronParser.Engine.ResultProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace CronParser.Runner
{
    public static class Startup
    {
        public static IServiceCollection BuildServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            // Parsing
            serviceCollection.AddTransient<IStepValueParser, StepValueParser>();
            serviceCollection.AddTransient<IListParser, ListParser>();
            serviceCollection.AddTransient<IRangeParser, RangeParser>();
            serviceCollection.AddTransient<ISimpleValueParser, SimpleValueParser>();
            serviceCollection.AddTransient<ICronElementParser, CronElementParser>();

            // Builder
            serviceCollection.AddTransient<ICronResultBuilder, CronResultBuilder>();

            // Formatting
            serviceCollection.AddTransient<ICronRowFormatter, CronRowFormatter>();
            
            // Orchestrator
            serviceCollection.AddTransient<ICronResultOrchestrator, CronResultOrchestrator>();

            return serviceCollection;
        }
    }
}