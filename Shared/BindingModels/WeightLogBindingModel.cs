using System;
using System.ComponentModel.DataAnnotations;
using WeightTrack.Shared.Models;

namespace WeightTrack.Shared.BindingModels
{
    public class WeightLogBindingModel
    {
        public static Func<WeightLog, WeightLogBindingModel> FromWeightLog = (log) =>
               new WeightLogBindingModel
               {
                   Id = log.Id,
                   UserId = log.UserId,
                   Weight = log.Weight
               };

        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [Range(5, 1000)]
        public decimal Weight { get; set; }
    }
}
