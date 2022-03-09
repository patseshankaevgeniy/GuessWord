using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace GuessWord.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _db;

        public TestController(ICurrentUserService currentUserService, ApplicationDbContext db)
        {
            _currentUserService = currentUserService;
            _db = db;
        }

        [Authorize(Policy = "admin")]
        [HttpGet]
        public ActionResult<string> GetHello()
        {
            var userRole = _db.Users
                .Include(x => x.UserRoles)
                .Where(x => x.Id == _currentUserService.UserId)
                .Select(x => x.UserRoles)
                .ToList();

            return Ok(_currentUserService.UserId);
        }

    }
}
