namespace CronParser.Engine.ResultProcessing
{
    public interface ICronResultBuilder
    {
        void Reset();
        CronResultBuilder WithInputString(string input);
        CronResultBuilder AddMinutes();
        CronResultBuilder AddHours();
        CronResultBuilder AddDayOfMonth();
        CronResultBuilder AddMonth();
        CronResultBuilder AddDayOfWeek();
        CronResultBuilder AddCommand();
        string Build();
    }
}