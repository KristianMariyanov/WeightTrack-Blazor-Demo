using System;
using WeightTrack.Shared.Models;
using WeightTrack.Shared.Models.Enums;

namespace WeightTrack.Shared.BindingModels
{
    public class FoodLogBindingModel
    {
        public static Func<FoodLog, FoodLogBindingModel> FromFoodLog = (foodLog) =>
            new FoodLogBindingModel
            {
                Id = foodLog.Id,
                Calories = foodLog.Calories,
                FoodName = foodLog.Food.Name,
                LoggedOn = foodLog.LoggedOn,
                Quantity = foodLog.Grams,
                Type = foodLog.FoodType,
                UserId = foodLog.UserId,
                FoodId = foodLog.FoodId,
            };

        public int Id { get; set; }

        public string UserId { get; set; }

        public string FoodName { get; set; }

        public int FoodId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime LoggedOn { get; set; }

        public FoodLogType Type { get; set; }

        public decimal Calories { get; set; }
    }
}
