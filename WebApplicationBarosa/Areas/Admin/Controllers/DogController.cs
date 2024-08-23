using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;
using WebApplicationBarosa.Models.ViewModels;
using WebApplicationBarosa.Utility;

namespace WebApplicationBarosa.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class DogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DogController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Dog> objDogList = _unitOfWork.Dog.GetAll(includeProperties:"Category").ToList();

            return View(objDogList);
        }

        public IActionResult Upsert(int? id)
        {
            DogVM dogVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.TypeOfBreed,
                    Value = u.CategoryId.ToString()
                }),
                Dog = new Dog()
            };
            if (id == null || id == 0)
            {
                return View(dogVM);
            }
            else
            {
                //za update
                dogVM.Dog = _unitOfWork.Dog.Get(u => u.Id == id);
                return View(dogVM);

            }

        }

        [HttpPost]
        public IActionResult Upsert(DogVM dogVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string dogPath = Path.Combine(wwwRootPath, @"images\dog");

                    if (!string.IsNullOrEmpty(dogVM.Dog.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, dogVM.Dog.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(dogPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    dogVM.Dog.ImageUrl = @"\images\dog\" + fileName;
                }

                if (dogVM.Dog.Id == 0)
                {
                    _unitOfWork.Dog.Add(dogVM.Dog);
                }
                else
                {
                    _unitOfWork.Dog.Update(dogVM.Dog);
                }

                _unitOfWork.Save();
                TempData["success"] = "Dog created successfully";
                return RedirectToAction("Index");
            }

            //deo gde se dodaje povratna vrednost ako ModelState nije validan
            dogVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.TypeOfBreed,
                Value = u.CategoryId.ToString()
            });

            return View(dogVM);  // vraćanje view-a sa nevalidnim modelom
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<Dog> objDogList = _unitOfWork.Dog.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objDogList });
        }

        public IActionResult Delete(int? id)
        {
            var dogToBeDeleted = _unitOfWork.Dog.Get(u=> u.Id == id);
            if (dogToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (!string.IsNullOrEmpty(dogToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, dogToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Dog.Remove(dogToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });

        }
        #endregion
    }
}
