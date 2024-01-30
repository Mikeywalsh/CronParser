using CronParser.Engine.Exceptions;

namespace CronParser.Engine.Parsing.SubParsers
{
    public class StepValueParser : IStepValueParser
    {
        /// <summary>
        /// Given an input cron string, will try to extract a step value from it. E.g 1 / 8 has a step value of 8
        /// </summary>
        /// <param name="input">The input cron string</param>
        /// <param name="minValue">The minimum allowed value for parsed results</param>
        /// <param name="maxValue">The max allowed value for parsed results</param>
        /// <param name="stepValue">The extracted step value</param>
        /// <returns>True if a step value was present</returns>
        /// <exception cref="CronParseException">Thrown if we ran into any issues whilst parsing</exception>
        public bool TryGetStepValue(string input, int minValue, int maxValue, out int stepValue)
        {
            var splitInput = input.Split('/');

            // Don't allow more than 1 step character
            if (splitInput.Length > 2)
            {
                throw new CronParseException("Input cannot contain more than one step character (/)");
            }

            // If there is no step character, return false immediately
            if (splitInput.Length == 1)
            {
                stepValue = 1;
                return false;
            }

            // We have a step value, attempt to parse it
            if (!int.TryParse(splitInput[1], out var parsedStepValue))
            {
                throw new CronParseException("Failed to parse stepValue for input field!");
            }

            if (parsedStepValue < minValue)
            {
                throw new CronParseException(
                    $"Step value ({parsedStepValue} is lower than min allowed value of {minValue}");
            }

            if (parsedStepValue > maxValue)
            {
                throw new CronParseException(
                    $"Value of step value ({parsedStepValue} exceeds max allowed value of {maxValue}");
            }

            stepValue = parsedStepValue;
            return true;
        }
    }
}