using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoGallery.Areas.Admin.Models;
using VideoGallery.Models;

namespace Repository.contract
{
    public class CategoryServices : ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Category CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return category;
        }

        public bool DeleteCategory(int id)
        {
            var del = context.Categories.SingleOrDefault(e => e.Id == id);
            if (del != null)
            {
                del.IsActive = false;
                context.Categories.Update(del);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Category> GetCategories()
        {
            var del = context.Categories.Where(e => e.IsActive == true).ToList();
            return del;

        }

        public Category GetCategoryById(int id)
        {
            var del = context.Categories.SingleOrDefault(e => e.IsActive == true && e.Id == id);
            return del;
        }

        public Category UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
            return category;
        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
