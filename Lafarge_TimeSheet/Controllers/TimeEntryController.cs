using Lafarge_TimeSheet.Context;
using Lafarge_TimeSheet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lafarge_TimeSheet.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _appDbContext;
        public TimeEntryController(UserManager<ApplicationUser> userManager, ApplicationDBContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }


        public async Task<IActionResult> ClockIn()
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist != null)
            {
                TimeEntry timeEntry = new TimeEntry()
                {
                    ClockInTime = DateTime.Now,
                    UserId = userId,
                    Time = DateTime.Now,
                };

                _appDbContext.TimeEntries.Add(timeEntry);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "ClockIn Successful";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> ClockOut()
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist != null)
            {
                TimeEntry timeEntry = new TimeEntry()
                {
                    ClockOutTime = DateTime.Now,
                    UserId = userId,
                    Time = DateTime.Now,
                };

                _appDbContext.TimeEntries.Add(timeEntry);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "ClockOut Successful";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Entries()
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var allEntries = await _appDbContext.TimeEntries.ToListAsync();

            var userEntries = allEntries.Where(x => x.UserId == userId).ToList();
            if (userEntries != null)
            {
                return View(userEntries.OrderByDescending(x => x.Time));
            }
            return View();
        }
    }
}
