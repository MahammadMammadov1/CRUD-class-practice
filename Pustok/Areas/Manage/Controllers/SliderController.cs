using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Models;

namespace Pustok.Areas.Manage.Controllers
{ 
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _slider;

        public SliderController(AppDbContext appDb)
        {
            _slider = appDb;    
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _slider.Sliders.ToList();
            return View(sliderList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            _slider.Sliders.Add(slider);
            _slider.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id ==id);

            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == slider.Id) ;

            wanted.Title = slider.Title;
            wanted.Description = slider.Description;
            wanted.RedirctText = slider.RedirctText;
            wanted.RedirctUrl = slider.RedirctUrl;
            wanted.ImageUrl = slider.ImageUrl;

            _slider.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == id);

            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            _slider.Sliders.Remove(wanted);
            _slider.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
