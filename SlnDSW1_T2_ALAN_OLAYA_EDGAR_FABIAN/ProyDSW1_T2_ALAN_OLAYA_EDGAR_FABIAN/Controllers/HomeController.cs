using Microsoft.AspNetCore.Mvc;
using ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Models;
using System.Diagnostics;

namespace ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
