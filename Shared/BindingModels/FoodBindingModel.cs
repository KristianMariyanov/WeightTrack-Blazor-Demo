using System;
using WeightTrack.Shared.Models;

namespace WeightTrack.Shared.BindingModels
{
    public class FoodBindingModel
    {
        public static Func<Food, FoodBindingModel> FromFood = (food) =>
             new FoodBindingModel
             {
                 Id = food.Id,
                 CaloriesPer100Gr = food.CaloriesPer100Gr,
                 Name = food.Name
             };

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal CaloriesPer100Gr { get; set; }
    }
}
