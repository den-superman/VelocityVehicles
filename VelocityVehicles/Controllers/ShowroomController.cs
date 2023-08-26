using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VelocityVehicles.Models;
using VelocityVehicles.Repositories;
using VelocityVehicles.Services;

namespace VelocityVehicles.Controllers
{
    public class ShowroomController : Controller
    {
        private readonly IShowroomRepository _showroomRepository;
        private readonly UserManager<User> _userManager;
        private readonly DBContext _db;


        public ShowroomController(IShowroomRepository showroomRepository, UserManager<User> userManager, DBContext db) { 
            _showroomRepository = showroomRepository;
            _userManager = userManager;
            _db = db;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var shows = await _showroomRepository.GetAllAsync();
            return View(shows);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Showroom showroom)
        {
            var newShowroom = new Showroom()
            {
                Providers = showroom.Providers,
                ShowroomDescription = showroom.ShowroomDescription,
                ShowroomName = showroom.ShowroomName
            };
            await _showroomRepository.AddNewAsync(newShowroom);

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            var show = await _showroomRepository.GetShowroomAsync(id);
            return View(show);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var show = await _showroomRepository.GetShowroomAsync(id);
            return View(show);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _showroomRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var show = await _showroomRepository.GetShowroomAsync(id);
            var editshow = new Showroom()
            {
                ShowroomDescription = show.ShowroomDescription,
                Providers = show.Providers,
                ShowroomName = show.ShowroomName
            };
            return View(editshow);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Showroom showroom)
        {
            var show = await _showroomRepository.GetShowroomAsync(id);
            show.ShowroomDescription = showroom.ShowroomDescription;
            show.ShowroomName = showroom.ShowroomName;
            await _showroomRepository.UpdateAsync(show);
            return RedirectToAction(nameof(Index));
        }
    }
}
