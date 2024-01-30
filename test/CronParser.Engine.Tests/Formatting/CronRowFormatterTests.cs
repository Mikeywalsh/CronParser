using CronParser.Engine.Formatting;
using Xunit;

namespace CronParser.Engine.Tests.Formatting
{
    public class CronRowFormatterTests
    {
        private readonly ICronRowFormatter _sut;

        public CronRowFormatterTests()
        {
            _sut = new CronRowFormatter();
        }

        [Fact]
        public void Format_CorrectlyFormats()
        {
            // Arrange
            var col1Input = "minute";
            var col2Input = "5 10 15";
            var expectedOutput = "minute        5 10 15";

            // Act
            var result = _sut.Format(col1Input, col2Input);

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    }
}