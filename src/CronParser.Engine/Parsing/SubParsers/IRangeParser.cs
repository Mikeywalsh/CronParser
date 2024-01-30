using CronParser.Engine.Structs;

namespace CronParser.Engine.Parsing.SubParsers
{
    public interface IRangeParser
    {
        bool TryGetRange(string input, int minValue, int maxValue, out CronRange cronRange);
    }
}