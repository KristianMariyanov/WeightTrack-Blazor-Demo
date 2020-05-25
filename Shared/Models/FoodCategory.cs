using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Shared.Models
{
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string ImageInBase64 { get; set; }

        public IEnumerable<Food> Foods { get; set; }
    }
}
