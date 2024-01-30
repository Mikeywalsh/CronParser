using CronParser.Engine.Enums;
using CronParser.Engine.Exceptions;
using CronParser.Engine.Structs;

namespace CronParser.Engine.Parsing.SubParsers
{
    public class SimpleValueParser : ISimpleValueParser
    {
        /// <summary>
        /// Given an input cron string, will extract a simple value from it. E.g 5 is a simple value, as is *
        /// If a step character is present, we only check the left side of it
        /// </summary>
        /// <param name="input">The input cron string</param>
        /// <param name="minValue">The minimum allowed value for parsed results</param>
        /// <param name="maxValue">The max allowed value for parsed results</param>
        /// <returns>A <see cref="SimpleCronValue"/>, parsed from the input, which can be either an asterisk or a numerical value</returns>
        /// <exception cref="CronParseException">Thrown if we ran into any issues whilst parsing</exception>
        public SimpleCronValue GetSimpleValue(string input, int minValue, int maxValue)
        {
            var splitInput = input.Split('/');

            // We only care what's on the left of a step character (if there even is one)
            var valueString = splitInput[0];

            var wasParsedToInt = int.TryParse(valueString, out var parsedValue);

            if (!wasParsedToInt)
            {
                // Don't fail yet, could still be an asterisk value
                if (valueString == "*")
                {
                    return new SimpleCronValue(SimpleCronValueType.ASTERISK);
                }

                // If we made it here then we failed to parse
                throw new CronParseException($"Failed to parse simple cron value from input: {valueString}");
            }

            if (parsedValue < minValue)
            {
                throw new CronParseException(
                    $"Simple value ({parsedValue} is lower than min allowed value of {minValue}");
            }
        
            if (parsedValue > maxValue)
            {
                throw new CronParseException(
                    $"Simple value ({parsedValue} exceeds max allowed value of {maxValue}");
            }

            return new SimpleCronValue(SimpleCronValueType.NUMBER, parsedValue);
        }
    }
}