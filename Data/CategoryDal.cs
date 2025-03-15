using System;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class CategoryDal : ICategory
{
    private List<Category> _categories = new List<Category>();

    public CategoryDal()
    {
        _categories = new List<Category>{
            new Category{CategoryId=1, CategoryName="Beverages"},
            new Category{CategoryId=2, CategoryName="Condiments"},
            new Category{CategoryId=3, CategoryName="Confections"},
            new Category{CategoryId=4, CategoryName="Dairy Products"},
            new Category{CategoryId=5, CategoryName="Grains/Cereals"},
            new Category{CategoryId=6, CategoryName="Meat/Poultry"},
            new Category{CategoryId=7, CategoryName="Produce"},
            new Category{CategoryId=8, CategoryName="Seafood"}
        };
    }
    public List<Category> GetCategories()
    {
        return _categories;
    }
    public Category GetCategoryById(int categoryId)
    {
        var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        return category;
    }

    public Category InsertCategory(Category category)
    {
        _categories.Add(category);
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        var existingCategory = GetCategoryById(category.CategoryId);
        if (existingCategory != null)
            existingCategory.CategoryName = category.CategoryName;
        else
            throw new Exception("Category not found");
        return existingCategory;
    }

    public void DeleteCategory(int categoryId)
    {
        var category = GetCategoryById(categoryId);
        if (category != null)
        {
            _categories.Remove(category);
        }
    }
}