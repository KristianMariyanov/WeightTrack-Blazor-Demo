using System;
using System.ComponentModel.DataAnnotations;
using WeightTrack.Shared.Models.Enums;

namespace WeightTrack.Shared.Models
{
    public class FoodLog
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 10000)]
        public decimal Grams { get; set; }

        public FoodLogType FoodType { get; set; }

        [Range(1, 1000000)]
        public decimal Calories { get; set; }

        public DateTime LoggedOn{ get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int FoodId { get; set; }

        public Food Food { get; set; }
    }
}
