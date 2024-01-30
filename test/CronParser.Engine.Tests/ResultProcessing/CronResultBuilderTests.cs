using CronParser.Engine.Formatting;
using CronParser.Engine.Parsing;
using CronParser.Engine.ResultProcessing;
using NSubstitute;
using Xunit;

namespace CronParser.Engine.Tests.ResultProcessing
{
    public class CronResultBuilderTests
    {
        private readonly ICronElementParser _cronElementParser;
        private readonly ICronRowFormatter _cronRowFormatter;

        private readonly CronResultBuilder _sut;

        public CronResultBuilderTests()
        {
            _cronElementParser = Substitute.For<ICronElementParser>();
            _cronRowFormatter = Substitute.For<ICronRowFormatter>();
            _sut = new CronResultBuilder(_cronElementParser, _cronRowFormatter);
        }

        [Fact]
        public void Build_CorrectlyReturnsBuiltCronResultString()
        {
            // Arrange
            var input = "1 1 1 1 1 /usr/bin/find";

            _cronElementParser.ParseElement("1", Arg.Any<int>(), Arg.Any<int>()).Returns("1");

            _cronRowFormatter.Format("minute", "1").Returns("minute        1");
            _cronRowFormatter.Format("hour", "1").Returns("hour          1");
            _cronRowFormatter.Format("day of month", "1").Returns("day of month  1");
            _cronRowFormatter.Format("month", "1").Returns("month         1");
            _cronRowFormatter.Format("day of week", "1").Returns("day of week   1");
            _cronRowFormatter.Format("command", "/usr/bin/find").Returns("command       /usr/bin/find");

            var expectedOutput =
                "minute        1\r\nhour          1\r\nday of month  1\r\nmonth         1\r\nday of week   1\r\ncommand       /usr/bin/find\r\n";
            
            // Act
            var builder = _sut.WithInputString(input)
                .AddMinutes()
                .AddHours()
                .AddDayOfMonth()
                .AddMonth()
                .AddDayOfWeek()
                .AddCommand();

            var result = builder.Build();

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    }
}