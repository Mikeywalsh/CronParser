using System.Collections.Generic;
using CronParser.Engine.Exceptions;
using CronParser.Engine.Parsing.SubParsers;
using Xunit;

namespace CronParser.Engine.Tests.Parsing.SubParsers
{
    public class ListParserTests
    {
        private readonly IListParser _sut;
        
        public ListParserTests()
        {
            _sut = new ListParser();
        }
        
        [Fact]
        public void TryGetList_CorrectlyParsesList()
        {
            // Arrange
            var input = "1, 3, 5, 8";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = new List<int>
            {
                1, 3, 5, 8
            };
            
            // Act
            var wasParsedCorrectly = _sut.TryGetList(input, minValue, maxValue, out var outputList);
            
            // Assert
            Assert.True(wasParsedCorrectly);
            Assert.Equivalent(expectedOutput, outputList);
        }

        [Fact]
        public void TryGetList_ReturnsFalse_WhenNoListPresent()
        {
            // Arrange
            var input = "1-5/9";
            var minValue = 1;
            var maxValue = 31;

            var expectedOutput = new List<int>
            {
                1, 3, 5, 8
            };
            
            // Act
            var wasParsedCorrectly = _sut.TryGetList(input, minValue, maxValue, out var outputList);
            
            // Assert
            Assert.False(wasParsedCorrectly);
        }
        
        [Fact]
        public void TryGetList_ThrowsException_WhenInputDataIsInvalid()
        {
            // Arrange
            var input = "1, 3, 5, 11";
            var minValue = 1;
            var maxValue = 10;

            // Act & Assert
            Assert.Throws<CronParseException>(() => _sut.TryGetList(input, minValue, maxValue, out _));
        }
    }
}