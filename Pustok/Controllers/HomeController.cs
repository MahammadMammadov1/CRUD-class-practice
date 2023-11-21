using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System.Diagnostics;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _logger;

        public HomeController(AppDbContext logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Sliders = _logger.Sliders.ToList();
            model.Services = _logger.Services.ToList();
            return View(model);
        }

       
        
    }
}