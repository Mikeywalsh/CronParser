namespace CronParser.Engine.ResultProcessing
{
    public interface ICronResultOrchestrator
    {
        string GetCronString(string input);
    }
}