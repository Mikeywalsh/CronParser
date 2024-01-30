using CronParser.Engine.ResultProcessing;
using CronParser.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CronParser.Engine.IntegrationTests
{
    public class CronParserIntegrationTests
    {
        private readonly ICronResultOrchestrator _cronResultOrchestrator;
        
        public CronParserIntegrationTests()
        {
            var serviceProvider = Startup.BuildServiceCollection().BuildServiceProvider();
            _cronResultOrchestrator = serviceProvider.GetService<ICronResultOrchestrator>()!;
        }

        [Fact]
        public void GetCronString_ReturnsValueInAssignment()
        {
            // Arrange
            var input = "*/15 0 1,15 * 1-5 /usr/bin/find";

            var expectedOutput =
                "minute        0 15 30 45\r\nhour          0\r\nday of month  1 15\r\nmonth         1 2 3 4 5 6 7 8 9 10 11 12\r\nday of week   1 2 3 4 5\r\ncommand       /usr/bin/find\r\n";
            
            // Act
            var result = _cronResultOrchestrator.GetCronString(input);

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    }
}