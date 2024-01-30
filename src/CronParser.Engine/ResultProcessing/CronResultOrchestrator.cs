namespace CronParser.Engine.ResultProcessing
{
    public class CronResultOrchestrator : ICronResultOrchestrator
    {
        private readonly ICronResultBuilder _cronResultBuilder;

        public CronResultOrchestrator(ICronResultBuilder cronResultBuilder)
        {
            _cronResultBuilder = cronResultBuilder;
        }

        public string GetCronString(string input)
        {
            var builder = _cronResultBuilder.WithInputString(input)
                .AddMinutes()
                .AddHours()
                .AddDayOfMonth()
                .AddMonth()
                .AddDayOfWeek()
                .AddCommand();

            return builder.Build();
        }
    }
}