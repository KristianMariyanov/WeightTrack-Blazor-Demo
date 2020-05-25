using WeightTrack.Client.Services;
using Xunit;

namespace WeightTrack.Tests
{
    public class ServicesTests
    {
        [Fact]
        public void ChartDataGenerator_ShouldReturnCorrectResult()
        {
            // Arrange
            var service = new ChartDataGenerator();

            // Act
            var result = service.GenerateCaloriesChartData(600);

            // Assert
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal("Left Calories", item.SegmentName);
                    Assert.Equal(1600, item.SegmentValue);
                    Assert.Equal("#D8D8D8", item.Color);
                },
                item =>
                {
                    Assert.Equal("Today Calories", item.SegmentName);
                    Assert.Equal(600, item.SegmentValue);
                    Assert.Equal("#00ff00", item.Color);
                });
        }

    }
}
