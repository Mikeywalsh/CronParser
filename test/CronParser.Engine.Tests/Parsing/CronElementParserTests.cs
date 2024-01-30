using System.Collections.Generic;
using CronParser.Engine.Enums;
using CronParser.Engine.Parsing;
using CronParser.Engine.Parsing.SubParsers;
using CronParser.Engine.Structs;
using NSubstitute;
using Xunit;

namespace CronParser.Engine.Tests.Parsing
{
    public class CronElementParserTests
    {
        private readonly ISimpleValueParser _simpleValueParser;
        private readonly IStepValueParser _stepValueParser;
        private readonly IRangeParser _rangeParser;
        private readonly IListParser _listParser;

        private readonly ICronElementParser _sut;

        public CronElementParserTests()
        {
            _simpleValueParser = Substitute.For<ISimpleValueParser>();
            _stepValueParser = Substitute.For<IStepValueParser>();
            _rangeParser = Substitute.For<IRangeParser>();
            _listParser = Substitute.For<IListParser>();

            _sut = new CronElementParser(_simpleValueParser, _stepValueParser, _rangeParser, _listParser);
        }

        [Fact]
        public void ParseElement_ReturnsExpectedCronString()
        {
            // Arrange
            var input = "5/10";
            var minValue = 0;
            var maxValue = 59;

            var expectedOutput = "5 15 25 35 45 55";

            _simpleValueParser.GetSimpleValue(input, minValue, maxValue)
                .Returns(new SimpleCronValue(SimpleCronValueType.NUMBER, 5));
            _stepValueParser.TryGetStepValue(input, minValue, maxValue, out Arg.Any<int>()).Returns(x =>
            {
                x[3] = 10;
                return true;
            });
            _rangeParser.TryGetRange(input, minValue, maxValue, out Arg.Any<CronRange>()).Returns(false);
            _listParser.TryGetList(input, minValue, maxValue, out Arg.Any<IList<int>>()).Returns(false);

            // Act
            var result = _sut.ParseElement(input, minValue, maxValue);

            // Assert
            Assert.Equal(expectedOutput, result);
        }
        
        // I WOULD ADD A BUNCH OF OTHER TEST CASES HERE, BUT I THINK IT'S OUTSIDE THE SCOPE OF THE ASSIGNMENT, AS IT WOULD TAKE TOO MUCH TIME
        // OTHER TESTS WOULD LOOK SIMILAR TO THE ABOVE HOWEVER, SO THERE IS NOTHING MORE TO DEMONSTRATE HERE
    }
}