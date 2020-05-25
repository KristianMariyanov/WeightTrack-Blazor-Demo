using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeightTrack.Server.Data;
using System.Collections.Generic;
using System.Linq;
using WeightTrack.Shared.BindingModels;
using Microsoft.AspNetCore.Identity;
using WeightTrack.Shared.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeightTrack.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FoodsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public FoodsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<FoodBindingModel>> Get(int? categoryId = null)
        {
            var result = (await _dbContext.Foods
                .Where(x => !categoryId.HasValue || x.CategoryId == categoryId)
                .ToListAsync())
                .Select(FoodBindingModel.FromFood);

            return result;
        }
    }
}
