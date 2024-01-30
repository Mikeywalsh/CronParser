namespace CronParser.Engine.Parsing.SubParsers
{
    public interface IStepValueParser
    {
        bool TryGetStepValue(string input, int minValue, int maxValue, out int stepValue);
    }
}