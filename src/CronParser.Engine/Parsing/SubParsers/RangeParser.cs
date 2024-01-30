using CronParser.Engine.Exceptions;
using CronParser.Engine.Structs;

namespace CronParser.Engine.Parsing.SubParsers
{
    public class RangeParser : IRangeParser
    {
        /// <summary>
        /// Given an input cron string, will try to extract a range  from it. E.g 1-5 returns a range with start 1 and end 5
        /// </summary>
        /// <param name="input">The input cron string</param>
        /// <param name="minValue">The minimum allowed value for parsed results</param>
        /// <param name="maxValue">The max allowed value for parsed results</param>
        /// <param name="cronRange">The extracted range</param>
        /// <returns>True if a range was present</returns>
        /// <exception cref="CronParseException">Thrown if we ran into any issues whilst parsing</exception>
        public bool TryGetRange(string input, int minValue, int maxValue, out CronRange cronRange)
        {
            // We only care about everything before the step character (if there even is one)
            var steplessInput = input.Split('/')[0];

            var splitInput = steplessInput.Split('-');

            // Only one range specification character is allowed
            if (splitInput.Length > 2)
            {
                throw new CronParseException("Only one range specifier character (-) is allowed");
            }

            // Parse the lower and upper range limits
            if (splitInput.Length == 2)
            {
                if (!int.TryParse(splitInput[0], out var min))
                {
                    throw new CronParseException($"Failed to parse min argument in range specification ({splitInput[0]}");
                }

                if (!int.TryParse(splitInput[1], out var max))
                {
                    throw new CronParseException($"Failed to parse max argument in range specification ({splitInput[1]}");
                }

                if (min < minValue)
                {
                    throw new CronParseException(
                        $"Value of lower range specifier ({min} is lower than min allowed value of {minValue}");
                }

                if (max > maxValue)
                {
                    throw new CronParseException(
                        $"Value of upper range specifier ({max}) exceeds max allowed value of {maxValue}");
                }

                if (max <= min)
                {
                    throw new CronParseException(
                        $"Value of upper range specifier ({{max}}) must be greater than lower range specifier ({min})");
                }

                cronRange = new CronRange(min, max);
                return true;
            }

            // There is no range, return false
            cronRange = default;
            return false;
        }
    }
}