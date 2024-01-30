using CronParser.Engine.Structs;

namespace CronParser.Engine.Parsing.SubParsers
{
    public interface ISimpleValueParser
    {
        SimpleCronValue GetSimpleValue(string input, int minValue, int maxValue);
    }
}