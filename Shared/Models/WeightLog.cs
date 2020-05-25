using System;
using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Shared.Models
{
    public class WeightLog
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 1000)]
        public decimal Weight { get; set; }

        public DateTime LoggedOn { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
