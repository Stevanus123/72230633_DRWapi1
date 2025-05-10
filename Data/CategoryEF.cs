using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class CategoryEF : ICategory
{
    private readonly ApplicationDbContext _context;
    public CategoryEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public void DeleteCategory(int categoryId)
    {
        var category = GetCategoryById(categoryId);
        if (category == null)
        {
            throw new Exception($"Kategori sing ID ne {categoryId} mboten wonten.");
        }
        try
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Kategori gagal dihapus: {ex.Message}");
        }
    }


    public List<Category> GetCategories()
    {
        var categories = _context.Categories.OrderByDescending(c => c.CategoryId)
        .ToList();
        return categories;
    }


    public Category GetCategoryById(int categoryId)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        if (category == null)
        {
            throw new Exception($"Kategori sing ID ne {categoryId} mboten wonten.");
        }
        return category;
    }

    public Category InsertCategory(Category category)
    {
        try
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }
        catch (Exception ex)
        {
            throw new Exception($"Kategori gagal disimpen: {ex.Message}");
        }
    }

    public Category UpdateCategory(Category category)
    {
        var existingCategory = GetCategoryById(category.CategoryId);
        if (existingCategory == null)
        {
            throw new Exception($"Kategori sing ID ne {category.CategoryId} mboten wonten.");
        }
        try
        {
            existingCategory.CategoryName = category.CategoryName;
            _context.Categories.Update(existingCategory);
            _context.SaveChanges();
            return existingCategory;
        }
        catch (Exception ex)
        {
            throw new Exception($"Kategori gagal diupdate: {ex.Message}");
        }
    }
}
