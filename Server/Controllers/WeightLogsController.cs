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
    public class WeightLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeightLogsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public async Task<IActionResult> GetUserLogs(DateTime? date)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!date.HasValue)
            {
                date = DateTime.UtcNow;
            }

            var result = (await _dbContext.WeightLogs
                .Where(x => x.UserId == user.Id && x.LoggedOn.Date == date.Value.Date)
                .ToListAsync())
                .Select(WeightLogBindingModel.FromWeightLog);

            return Ok(result);
        }

        public async Task<IActionResult> Add([FromBody] WeightLogBindingModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var existingLogForToday = await _dbContext.WeightLogs.AnyAsync(l => l.LoggedOn.Date == DateTime.UtcNow.Date && l.UserId == user.Id);

            if (existingLogForToday)
            {
                return BadRequest("You already log your weight today");
            }

            var log = new WeightLog()
            {
                LoggedOn = DateTime.UtcNow,
                UserId = user.Id,
                Weight = model.Weight
            };

            _dbContext.WeightLogs.Add(log);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        public async Task<IActionResult> Delete([FromBody] WeightLogBindingModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var log = _dbContext.WeightLogs.Find(model.Id);
            if (log.UserId != user.Id)
            {
                return Unauthorized("You are not authorized to delete this log");
            }

            _dbContext.WeightLogs.Remove(log);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
