using System;
using System.Collections;
using System.Collections.Generic;
using WeightTrack.Client.Common;

namespace WeightTrack.Client.Services
{
    public class ChartDataGenerator
    {
        public IEnumerable<PieChartModel> GenerateCaloriesChartData(decimal totalCalories)
        {
            return new List<PieChartModel>
            {
                new PieChartModel
                {
                    SegmentName = totalCalories > 2200 ? "Exceeded Calories" : "Left Calories",
                    SegmentValue = Math.Abs(2200 - totalCalories),
                    Color= totalCalories > 2200 ? "#ff471a" : "#D8D8D8"
                },
                new PieChartModel
                {
                    SegmentName = "Today Calories",
                    SegmentValue = totalCalories,
                    Color= "#00ff00"
                }
            };
        }
    }
}
