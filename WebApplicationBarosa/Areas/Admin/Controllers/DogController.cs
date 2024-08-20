﻿using Microsoft.AspNetCore.Mvc;
using WebApplicationBarosa.DataAccess.Repository.IRepository;
using WebApplicationBarosa.Models;

namespace WebApplicationBarosa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
;

        }
        public IActionResult Index()
        {
            List<Dog> objDogList = _unitOfWork.Dog.GetAll().ToList();
            return View(objDogList);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Dog dog)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Dog.Add(dog);
                _unitOfWork.Save();
                TempData["success"] = "Dog created successfully";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(dog);
        }

        public IActionResult Edit(int id)
        {
            var dog = _unitOfWork.Dog.Get(d => d.Id == id);
            if (dog == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(dog);
        }

        [HttpPost]
        public IActionResult Edit(Dog dog)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Dog.Update(dog);
                _unitOfWork.Save();
                TempData["success"] = "Dog updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(dog);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Dog? dogFromDb = _unitOfWork.Dog.Get(u => u.Id == id);

            if (dogFromDb == null)
            {
                return NotFound();
            }
            return View(dogFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Dog? obj = _unitOfWork.Dog.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Dog.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Dog deleted successfully";
            return RedirectToAction("Index");
        }
    }

}
