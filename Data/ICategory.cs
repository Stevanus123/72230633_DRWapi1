using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface ICategory
{
    List<Category> GetCategories();

    Category GetCategoryById(int categoryId);

    Category InsertCategory(Category category);

    Category UpdateCategory(Category category);

    void DeleteCategory(int categoryId);

}
