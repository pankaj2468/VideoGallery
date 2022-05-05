using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoGallery.Areas.Admin.Models;
using VideoGallery.Areas.Admin.Models.ViewModels;
using VideoGallery.Repository;
using VideoGallery.Repository.Contract;

namespace VideoGallery.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategory categoryservice;
        private readonly IHostingEnvironment environment;

        public  CategoryController(ICategory category,IHostingEnvironment _environment)
        {
            this.categoryservice = category;
            environment = _environment;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            var path = environment.WebRootPath;
            var filePath = "contents/images/" + model.Image.FileName;
            var fullPath = Path.Combine(path, filePath);
            categoryservice.UploadFile(model.Image, fullPath);
            var Category = new Category()
            {
                Name = model.Name,  
                IsActive=model.IsActive,
                Created_On=DateTime.Now,
                Image=filePath
            };
            categoryservice.CreateCategory(Category);
            return RedirectToAction("ShowCategory");
        }

        public IActionResult ShowCategory()
        {
            var cats = categoryservice.GetCategories();
            return View(cats);
        }
        public IActionResult Delete(int id)
        {
            var del = categoryservice.DeleteCategory(id);
            if (del)
            {
               
                return RedirectToAction("ShowCategory");
            }
            else
            {
                ViewBag.message= "category not found !";
                return RedirectToAction("ShowCategory");
            }
           
        } 

        public IActionResult Edit(int id)
        {
            var cats = categoryservice.GetCategoryById(id);
            var cat2 = new CategoryViewModel()
            {
                Id = cats.Id,
                OldImage=cats.Image,
                IsActive=cats.IsActive,
                Name=cats.Name
            };
            return View(cat2);
        }

        [HttpPost]
       public IActionResult Edit(CategoryViewModel model)
        {
            var cat = categoryservice.GetCategoryById(model.Id);
            var path = environment.WebRootPath;
            var filePath = "contents/images/" + model.Image.FileName;
            var fullPath = Path.Combine(path, filePath);
            categoryservice.UploadFile(model.Image, fullPath);
            if (cat != null)
            {
                cat.Name = model.Name;
                cat.IsActive = model.IsActive;
                cat.Updated_On = DateTime.Now;
                cat.Image = filePath;
                categoryservice.UpdateCategory(cat);
                return RedirectToAction("ShowCategory");
            }
            else
            {
                ViewBag.message = "category not found";
                return View();

            }
            //return View();
        }
    }
}
