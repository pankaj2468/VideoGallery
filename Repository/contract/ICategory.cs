using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VideoGallery.Areas.Admin.Models;

namespace Repository.contract
{
    public interface ICategory
    {
        Category CreateCategory(Category category);
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        Category UpdateCategory(Category category);
        bool DeleteCategory(int id);
        public void UploadFile(IFormFile file, string path);

    }
}
