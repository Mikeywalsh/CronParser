using CronParser.Engine.Exceptions;
using CronParser.Engine.Parsing.SubParsers;
using Xunit;

namespace CronParser.Engine.Tests.Parsing.SubParsers
{
    public class StepParserTests
    {
        private readonly IStepValueParser _sut;

        public StepParserTests()
        {
            _sut = new StepValueParser();
        }

        [Fact]
        public void TryGetStep_CorrectlyParsesStep()
        {
            // Arrange
            var input = "1/5";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = 5;

            // Act
            var wasParsedCorrectly = _sut.TryGetStepValue(input, minValue, maxValue, out var outputStepValue);

            // Assert
            Assert.True(wasParsedCorrectly);
            Assert.Equal(expectedOutput, outputStepValue);
        }

        [Fact]
        public void TryGetStep_ReturnsFalse_WhenNoStepPresent()
        {
            // Arrange
            var input = "5";
            var minValue = 1;
            var maxValue = 31;

            // Act
            var wasParsedCorrectly = _sut.TryGetStepValue(input, minValue, maxValue, out _);

            // Assert
            Assert.False(wasParsedCorrectly);
        }

        [Fact]
        public void TryGetStep_ThrowsException_WhenInputDataIsInvalid()
        {
            // Arrange
            var input = "1/2/3";
            var minValue = 1;
            var maxValue = 10;

            // Act & Assert
            Assert.Throws<CronParseException>(() => _sut.TryGetStepValue(input, minValue, maxValue, out _));
        }
    }
}