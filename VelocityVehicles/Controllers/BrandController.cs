using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VelocityVehicles.Models;
using VelocityVehicles.Repositories;
using VelocityVehicles.Services;

namespace VelocityVehicles.Controllers
{
    public class BrandController : Controller
    {
        private readonly DBContext _db;
        private readonly IBrandRepository _brandRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAutomobileRepository _automobileRepository;

        public BrandController(DBContext db, IBrandRepository brandRepository, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IAutomobileRepository automobileRepository)
        {
            _db = db;
            _brandRepository = brandRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _automobileRepository = automobileRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAllAsync();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand, IFormFile file)
        {
            var imagePath = await SaveImageAsync(file);
            var newBrand = new Brand()
            {
                Automobiles = brand.Automobiles,
                BrandDescription = brand.BrandDescription,
                BrandName = brand.BrandName,
                ImagePath = imagePath
            };
            await _brandRepository.AddNewAsync(newBrand);
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
        public async Task<IActionResult> Details(int id)
        {
            var brand = await _brandRepository.GetBrandAsync(id);
            return View(brand);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetBrandAsync(id);
            return View(brand);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _brandRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetBrandAsync(id);
            var editbrand = new Brand()
            {
                Automobiles = brand.Automobiles,
                BrandDescription = brand.BrandDescription,
                BrandName = brand.BrandName,
                ImagePath = brand.ImagePath
            };
            return View(editbrand);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            var br = await _brandRepository.GetBrandAsync(id);
            br.BrandName = brand.BrandName;
            br.BrandDescription = brand.BrandDescription;
            await _brandRepository.UpdateAsync(br);
            return RedirectToAction(nameof(Index));
        }
    }
}
