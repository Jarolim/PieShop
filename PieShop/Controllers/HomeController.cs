using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieReposotory;

        public HomeController(IPieRepository pieRepository)
        {
            _pieReposotory = pieRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //ViewBag.Title = "Pie Overview";

            var pies = _pieReposotory.GetAllPies().OrderBy(p => p.Name);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Welcome to Pie Shop",
                Pies = pies.ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult Details(int Id)
        {
            var pie = _pieReposotory.GetPieById(Id);
            if (pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }
    }
}
