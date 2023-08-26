using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VelocityVehicles.Models;
using VelocityVehicles.Repositories;
using VelocityVehicles.Services;

namespace VelocityVehicles.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IShowroomRepository _showroomRepository;
        private readonly IAutomobileRepository _automobileRepository;
        private readonly DBContext _db;

        public ProviderController(IProviderRepository providerRepository, IShowroomRepository showroomRepository, IAutomobileRepository automobileRepository, DBContext dB)
        {
            _providerRepository = providerRepository;
            _automobileRepository = automobileRepository;
            _db = dB;
            _showroomRepository = showroomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var con = await _providerRepository.GetAllAsync();
            return View(con);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ShowroomId"] = new SelectList(await _showroomRepository.GetAllAsync(), "Id", "ShowroomName");
            ViewData["AutomobileId"] = new SelectList(await _automobileRepository.GetAllAsync(), "Id", "AutomobileName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Provider provider)
        {
            var prov = new Provider()
            {
                Automobile = provider.Automobile,
                AutomobileId = provider.AutomobileId,
                Showroom = provider.Showroom,
                ShowroomId = provider.ShowroomId
            };
            await _providerRepository.AddNewAsync(provider);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var prov = await _providerRepository.GetProviderAsync(id);
            return View(prov);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _providerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var prov = await _providerRepository.GetProviderAsync(id);
            return View(prov);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prov = await _providerRepository.GetProviderAsync(id);
            ViewData["ShowroomId"] = new SelectList(await _showroomRepository.GetAllAsync(), "Id", "ShowroomName");
            ViewData["AutomobileId"] = new SelectList(await _automobileRepository.GetAllAsync(), "Id", "AutomobileName");
            return View(prov);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Provider provider)
        {
            var prov = await _providerRepository.GetProviderAsync(id);
            prov.Showroom = provider.Showroom;
            prov.Automobile = provider.Automobile;
            await _providerRepository.UpdateAsync(prov);
            return View(prov);
        }
    }
}
