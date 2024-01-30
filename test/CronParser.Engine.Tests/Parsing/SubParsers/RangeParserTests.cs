using CronParser.Engine.Exceptions;
using CronParser.Engine.Parsing.SubParsers;
using CronParser.Engine.Structs;
using Xunit;

namespace CronParser.Engine.Tests.Parsing.SubParsers
{
    public class RangeParserTests
    {
        private readonly IRangeParser _sut;
        
        public RangeParserTests()
        {
            _sut = new RangeParser();
        }
        
        [Fact]
        public void TryGetRange_CorrectlyParsesRange()
        {
            // Arrange
            var input = "1-5";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = new CronRange(1, 5);
            
            // Act
            var wasParsedCorrectly = _sut.TryGetRange(input, minValue, maxValue, out var outputRange);
            
            // Assert
            Assert.True(wasParsedCorrectly);
            Assert.Equal(expectedOutput, outputRange);
        }

        [Fact]
        public void TryGetRange_ReturnsFalse_WhenNoRangePresent()
        {
            // Arrange
            var input = "5";
            var minValue = 1;
            var maxValue = 31;

            // Act
            var wasParsedCorrectly = _sut.TryGetRange(input, minValue, maxValue, out _);
            
            // Assert
            Assert.False(wasParsedCorrectly);
        }
        
        [Fact]
        public void TryGetRange_ThrowsException_WhenInputDataIsInvalid()
        {
            // Arrange
            var input = "1-11";
            var minValue = 1;
            var maxValue = 10;

            // Act & Assert
            Assert.Throws<CronParseException>(() => _sut.TryGetRange(input, minValue, maxValue, out _));
        }
    }
}