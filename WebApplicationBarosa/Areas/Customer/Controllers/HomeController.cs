using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Dog> dogList = _unitOfWork.Dog.GetAll(includeProperties:"Category");
            return View(dogList);
        }

        public IActionResult Details(int dogId)
        {
            Dog dog = _unitOfWork.Dog.Get(u=>u.Id== dogId, includeProperties: "Category");
            return View(dog);
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
