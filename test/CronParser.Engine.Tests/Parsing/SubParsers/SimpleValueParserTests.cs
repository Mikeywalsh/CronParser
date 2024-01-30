using CronParser.Engine.Enums;
using CronParser.Engine.Exceptions;
using CronParser.Engine.Parsing.SubParsers;
using CronParser.Engine.Structs;
using Xunit;

namespace CronParser.Engine.Tests.Parsing.SubParsers
{
    public class SimpleValueParserTests
    {
        private readonly ISimpleValueParser _sut;

        public SimpleValueParserTests()
        {
            _sut = new SimpleValueParser();
        }

        [Fact]
        public void GetSimpleValue_CorrectlyParsesSimpleAsteriskValue()
        {
            // Arrange
            var input = "*";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = new SimpleCronValue(SimpleCronValueType.ASTERISK);

            // Act
            var result = _sut.GetSimpleValue(input, minValue, maxValue);

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    
        [Fact]
        public void GetSimpleValue_CorrectlyParsesSimpleNumericValue()
        {
            // Arrange
            var input = "5";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = new SimpleCronValue(SimpleCronValueType.NUMBER, 5);

            // Act
            var result = _sut.GetSimpleValue(input, minValue, maxValue);

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void GetSimpleValue_ThrowsException_WhenNoSimpleValuePresent()
        {
            // Arrange
            var input = "5-10";
            var minValue = 1;
            var maxValue = 31;

            // Act & Assert
            Assert.Throws<CronParseException>(() =>_sut.GetSimpleValue(input, minValue, maxValue));
        }

        [Fact]
        public void GetSimpleValue_ThrowsException_WhenInputDataIsInvalid()
        {
            // Arrange
            var input = "1asdf";
            var minValue = 1;
            var maxValue = 10;

            // Act & Assert
            Assert.Throws<CronParseException>(() =>_sut.GetSimpleValue(input, minValue, maxValue));
        }
    }
}