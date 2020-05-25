using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Shared.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string BrandName { get; set; }

        [Range(0, 1000)]
        public decimal CaloriesPer100Gr { get; set; }

        public bool IsVegitarian { get; set; }

        public bool IsPublic { get; set; }

        public int CategoryId { get; set; }

        public FoodCategory Category { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
