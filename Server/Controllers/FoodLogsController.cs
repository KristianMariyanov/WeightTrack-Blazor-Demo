using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeightTrack.Server.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using WeightTrack.Shared.BindingModels;
using Microsoft.AspNetCore.Identity;
using WeightTrack.Shared.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WeightTrack.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FoodLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public FoodLogsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }



        public async Task<IActionResult> GetUserCalories(DateTime? date)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!date.HasValue)
            {
                date = DateTime.UtcNow;
            }

            var result = await _dbContext.FoodLogs
                .Where(x => x.UserId == user.Id && x.LoggedOn.Date == date.Value.Date)
                .SumAsync(x => x.Calories);

            return Ok(result);
        }

        public async Task<IEnumerable<FoodLogBindingModel>> GetUserLogs(DateTime? date)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!date.HasValue)
            {
                date = DateTime.UtcNow;
            }

            var result = _dbContext.FoodLogs
                .Where(x => x.UserId == user.Id && x.LoggedOn.Date == date.Value.Date)
                .Include(x => x.Food)
                .ToList()
                .Select(FoodLogBindingModel.FromFoodLog);

            return result;
        }

        public async Task<IActionResult> GetForUpdate(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = (await _dbContext.FoodLogs
                .Where(x => x.Id == id)
                .Include(x => x.Food)
                .ToListAsync())
                .Select(FoodLogBindingModel.FromFoodLog)
                .FirstOrDefault();

            if (user.Id != result.UserId)
            {
                return Unauthorized("You are not authorized to edit this food log");
            }

            return Ok(result);
        }

        public async Task Add([FromBody] FoodLogUpdateBindingModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var food = _dbContext.Foods.Find(model.FoodId);

            var foodLog = new FoodLog()
            {
                FoodId = model.FoodId,
                Calories = food.CaloriesPer100Gr * model.Quantity / 100,
                Grams = model.Quantity,
                LoggedOn = DateTime.UtcNow,
                UserId = user.Id,
                FoodType = model.Type
            };

            _dbContext.FoodLogs.Add(foodLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IActionResult> Update([FromBody] FoodLogUpdateBindingModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var foodLog = _dbContext.FoodLogs.Find(model.Id);
            if (foodLog.UserId != user.Id)
            {
                return Unauthorized("You are not authorized to edit this food log");
            }

            var food = await _dbContext.Foods.FindAsync(model.FoodId);

            foodLog.Calories = food.CaloriesPer100Gr * model.Quantity / 100;
            foodLog.FoodId = food.Id;
            foodLog.FoodType = model.Type;
            foodLog.Grams = model.Quantity;

            _dbContext.FoodLogs.Update(foodLog);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        public async Task<IActionResult> Delete([FromBody] FoodLogUpdateBindingModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var foodLog = _dbContext.FoodLogs.Find(model.Id);
            if (foodLog.UserId != user.Id)
            {
                return Unauthorized("You are not authorized to delete this food log");
            }

            _dbContext.FoodLogs.Remove(foodLog);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
