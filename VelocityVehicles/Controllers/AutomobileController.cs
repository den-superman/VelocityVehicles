using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Drawing.Drawing2D;
using VelocityVehicles.Models;
using VelocityVehicles.Repositories;
using VelocityVehicles.Services;

namespace VelocityVehicles.Controllers
{
    public class AutomobileController : Controller
    {
        private readonly DBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAutomobileRepository _automobileRepository;
        private readonly IBrandRepository _brand;
        public AutomobileController(DBContext db, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IAutomobileRepository automobileRepository, IBrandRepository brand)
        {
            _db = db;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _automobileRepository = automobileRepository;
            _brand = brand;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cars = await _automobileRepository.GetAllAsync();
            return View(cars);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["BrandId"] = new SelectList(await _brand.GetAllAsync(), "Id", "BrandName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Automobile automobile, IFormFile file)
        {

            var imagePath = await SaveImageAsync(file);

            var NewCar = new Automobile()
            {
                AutomobileName = automobile.AutomobileName,
                ImagePath = imagePath,
                Brand = automobile.Brand,
                BrandId = automobile.BrandId,
                Price = automobile.Price,
                Providers = automobile.Providers
            };
            await _automobileRepository.AddNewAsync(NewCar);

            return RedirectToAction(nameof(Index));
        }
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/uploads/" + uniqueFileName;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            var car = await _automobileRepository.GetAutomobileAsync(id);
            return View(car);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await _automobileRepository.GetAutomobileAsync(id);
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _automobileRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _automobileRepository.GetAutomobileAsync(id);
            var editcar = new Automobile()
            {
                AutomobileName = car.AutomobileName,
                Brand = car.Brand,
                BrandId = car.BrandId,
                ImagePath = car.ImagePath,
                Price = car.Price,
                Providers = car.Providers
            };
            return View(editcar);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Automobile automobile)
        {
            var car = await _automobileRepository.GetAutomobileAsync(id);
            car.AutomobileName = automobile.AutomobileName;
            car.Price = automobile.Price;
            await _automobileRepository.UpdateAsync(car);
            return RedirectToAction(nameof(Index));
        }
    }
}
