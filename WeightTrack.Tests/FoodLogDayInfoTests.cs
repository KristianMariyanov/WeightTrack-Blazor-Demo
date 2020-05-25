using Bunit;
using Bunit.Rendering;
using System;
using System.Collections.Generic;
using WeightTrack.Client.Components;
using WeightTrack.Shared.BindingModels;
using WeightTrack.Shared.Models;
using WeightTrack.Shared.Models.Enums;
using Xunit;

namespace WeightTrack.Tests
{
    public class FoodLogDayInfoTests
    {
        [Fact]
        public void FoodLogDayInfo_ShouldRenderCorrectHtml()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<FoodLogDayInfo>(
                ComponentParameter.CreateParameter("Logs", new List<FoodLogBindingModel>() { new FoodLogBindingModel() { Calories = 100, FoodName = "Яйце", } }),
                ComponentParameter.CreateParameter("Title", "Snack"),
                ComponentParameter.CreateParameter("Type", FoodLogType.Snack));

            // Assert
            cut.MarkupMatches(@"<div class=""card"">
    <div class=""card-header"">
        Snack
    </div>
    <div class=""card-body"">
        <ul class=""list-group mb-3"">
            <li class=""list-group-item food-log"">
                <span class=""food-log-text"">Яйце - 100 kcal</span> 
                <span class=""food-log-actions"">
                    <a href=""/edit-food-log/0"" class=""btn"">Edit</a>
                    <button class=""btn"">Delete</button>
                </span>
            </li>
        </ul>

        <a href=""add-food-log/4"" class=""btn btn-primary"">Add Snack</a>
    </div>
</div>");
        }
    }
}
