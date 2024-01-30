using System;
using System.Text;
using CronParser.Engine.Exceptions;
using CronParser.Engine.Formatting;
using CronParser.Engine.Parsing;

namespace CronParser.Engine.ResultProcessing
{
    public class CronResultBuilder : ICronResultBuilder
    {
        private readonly ICronElementParser _cronElementParser;
        private readonly ICronRowFormatter _cronRowFormatter;

        private StringBuilder _resultStringBuilder;
        private string[] _splitInput;

        private const int MINUTE_INDEX = 0;
        private const int HOUR_INDEX = 1;
        private const int DAY_OF_MONTH_INDEX = 2;
        private const int MONTH_INDEX = 3;
        private const int DAY_OF_WEEK_INDEX = 4;
        private const int COMMAND_INDEX = 5;

        private const int EXPECTED_INPUT_ELEMENTS = 6;

        public CronResultBuilder(ICronElementParser cronElementParser, ICronRowFormatter cronRowFormatter)
        {
            _cronElementParser = cronElementParser;
            _cronRowFormatter = cronRowFormatter;

            _splitInput = Array.Empty<string>();
            _resultStringBuilder = new StringBuilder();
            Reset();
        }

        public void Reset()
        {
            _resultStringBuilder.Clear();
            _splitInput = Array.Empty<string>();
        }

        public CronResultBuilder WithInputString(string input)
        {
            _splitInput = input.Split(' ');

            if (_splitInput.Length != EXPECTED_INPUT_ELEMENTS)
            {
                throw new CronParseException(
                    $"Input string {input} has {_splitInput.Length}! Expected element count is: {EXPECTED_INPUT_ELEMENTS}");
            }

            return this;
        }

        public CronResultBuilder AddMinutes()
        {
            ValidateState();

            var cronString = _cronElementParser.ParseElement(_splitInput[MINUTE_INDEX], 0, 59);
            var formattedString = _cronRowFormatter.Format("minute", cronString);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public CronResultBuilder AddHours()
        {
            ValidateState();

            var cronString = _cronElementParser.ParseElement(_splitInput[HOUR_INDEX], 0, 23);
            var formattedString = _cronRowFormatter.Format("hour", cronString);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public CronResultBuilder AddDayOfMonth()
        {
            ValidateState();

            var cronString = _cronElementParser.ParseElement(_splitInput[DAY_OF_MONTH_INDEX], 1, 31);
            var formattedString = _cronRowFormatter.Format("day of month", cronString);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public CronResultBuilder AddMonth()
        {
            ValidateState();

            var cronString = _cronElementParser.ParseElement(_splitInput[MONTH_INDEX], 1, 12);
            var formattedString = _cronRowFormatter.Format("month", cronString);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public CronResultBuilder AddDayOfWeek()
        {
            ValidateState();

            var cronString = _cronElementParser.ParseElement(_splitInput[DAY_OF_WEEK_INDEX], 1, 7);
            var formattedString = _cronRowFormatter.Format("day of week", cronString);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public CronResultBuilder AddCommand()
        {
            ValidateState();

            // Assuming validation on the command format is not needed for this assignment
            var formattedString = _cronRowFormatter.Format("command", _splitInput[COMMAND_INDEX]);
            _resultStringBuilder.AppendLine(formattedString);
            return this;
        }

        public string Build()
        {
            var result = _resultStringBuilder.ToString();
            Reset();
            return result;
        }

        private void ValidateState()
        {
            if (_splitInput.Length == 0)
            {
                throw new Exception(
                    "You must define an input string using the WithInputString method before adding data!");
            }
        }
    }
}