using Bunit;
using Bunit.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Telerik.Blazor.Services;
using WeightTrack.Client.Components;
using WeightTrack.Client.Services;
using Xunit;
using Moq;

namespace WeightTrack.Tests
{
    public class CaloriesChartTests
    {
        [Fact]
        public void CaloriesChartShouldPopulateDataCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            ctx.Services.AddSingleton<ChartDataGenerator>();
            ctx.Services.AddSingleton<ITelerikStringLocalizer>(new Mock<ITelerikStringLocalizer>().Object);
            ctx.Services.AddSingleton<IJSRuntime>(new Mock<IJSRuntime>().Object);

            // Act
            var cut = ctx.RenderComponent<CaloriesChart>(ComponentParameter.CreateParameter("TodayCalories", 600m));


            // Assert
            var componentData = cut.Instance.Data;
            Assert.Collection(componentData,
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
