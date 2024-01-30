using System.Text;
using CronParser.Engine.Enums;
using CronParser.Engine.Exceptions;
using CronParser.Engine.Parsing.SubParsers;

namespace CronParser.Engine.Parsing
{
    public class CronElementParser : ICronElementParser
    {
        private readonly ISimpleValueParser _simpleValueParser;
        private readonly IStepValueParser _stepValueParser;
        private readonly IRangeParser _rangeParser;
        private readonly IListParser _listParser;

        public CronElementParser(ISimpleValueParser simpleValueParser, IStepValueParser stepValueParser, IRangeParser rangeParser, IListParser listParser)
        {
            _simpleValueParser = simpleValueParser;
            _stepValueParser = stepValueParser;
            _rangeParser = rangeParser;
            _listParser = listParser;
        }

        /// <summary>
        /// Given a cron element input string, if valid, returns a formatted output string. If not valid, throws a <see cref="CronParseException"/>
        /// For example, a cron string of 1-5, min value of 1 and max value of 7 will return: 1 2 3 4 5
        /// </summary>
        /// <param name="input">The input string to parse. E.g (1-5)</param>
        /// <param name="minValue">The minimum value for this element. E.g months minValue is 1, but minutes would be 0</param>
        /// <param name="maxValue">The maximum value for this element. E.g months maxValue is 12</param>
        /// <returns>The parsed cron element</returns>
        /// <exception cref="CronParseException">Thrown if this cron expression cannot be parsed</exception>
        public string ParseElement(string input, int minValue, int maxValue)
        {
            var hasStepValue = _stepValueParser.TryGetStepValue(input, minValue, maxValue, out var stepValue);
            var hasRange = _rangeParser.TryGetRange(input, minValue, maxValue, out var range);
            var hasList = _listParser.TryGetList(input, minValue, maxValue, out var listElements);

            // Cannot use a list & step value in the same expression
            if (hasStepValue && hasList)
            {
                throw new CronParseException(
                    $"Expression: {input} uses both a step value and list at the same time. This is not valid cron syntax!");
            }

            // Cannot use a range & list in the same expression
            if (hasRange && hasList)
            {
                throw new CronParseException(
                    $"Expression: {input} uses both a range and list at the same time. This is not valid cron syntax!");
            }

            var cronStringBuilder = new StringBuilder();

            // Deal with the list case and return
            if (hasList)
            {
                for (var i = 0; i < listElements.Count; i++)
                {
                    var listElement = listElements[i];
                    cronStringBuilder.Append(listElement);

                    if (i < listElements.Count - 1)
                    {
                        cronStringBuilder.Append(' ');
                    }
                }

                return cronStringBuilder.ToString();
            }

            int startValue;
            int endValue;

            if (hasRange)
            {
                startValue = range.Start;
                endValue = range.End;
            }
            else
            {
                // If there is no list or range, then the only option left is a simple value. E.g a single number or asterisk
                var simpleValue = _simpleValueParser.GetSimpleValue(input, minValue, maxValue);

                if (simpleValue.CronValueType == SimpleCronValueType.ASTERISK)
                {
                    startValue = minValue;
                    endValue = maxValue;
                }
                else if (simpleValue.CronValueType == SimpleCronValueType.NUMBER)
                {
                    startValue = simpleValue.Value;

                    if (hasStepValue)
                    {
                        endValue = maxValue;
                    }
                    else
                    {
                        endValue = startValue;
                    }
                }
                else
                {
                    throw new CronParseException(
                        $"Unexpected simple cron value type: {simpleValue.CronValueType.ToString()}");
                }
            }

            for (var i = startValue; i <= endValue; i += stepValue)
            {
                cronStringBuilder.Append(i);

                // Append a space after each element (except the last)
                if (i + stepValue <= endValue)
                {
                    cronStringBuilder.Append(' ');
                }
            }
            
            return cronStringBuilder.ToString();
        }
    }
}