using System.ComponentModel.DataAnnotations;
using WeightTrack.Shared.Models.Enums;

namespace WeightTrack.Shared.BindingModels
{
    public class FoodLogUpdateBindingModel
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        [Range(1, 10000, ErrorMessage="The {0} should be between {1} and {2} grams.")]
        public decimal Quantity { get; set; }

        public FoodLogType Type { get; set; }
    }
}
