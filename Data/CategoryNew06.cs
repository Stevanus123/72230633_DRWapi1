using System;
using MySql.Data.MySqlClient;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class CategoryNew06 : ICategory
{
    private readonly IConfiguration _configuration;
    private readonly string _connStr = string.Empty;

    public CategoryNew06(IConfiguration configuration)
    {
        _configuration = configuration;
        _connStr = _configuration.GetConnectionString("DefaultConnection");
    }

    public List<Category> GetCategories()
    {
        List<Category> categories = new List<Category>();
        using (MySqlConnection conn = new MySqlConnection(_connStr))
        {
            conn.Open();
            string sql = "SELECT * FROM category";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rd06 = cmd.ExecuteReader();
            while (rd06.Read())
            {
                Category category = new Category();
                category.CategoryId = rd06.GetInt32("CategoryId");
                category.CategoryName = rd06.GetString("CategoryName");
                categories.Add(category);
            }
            rd06.Close();
            cmd.Dispose();
        }
        return categories;
    }

    public Category GetCategoryById(int categoryId)
    {
        Category category = new Category();
        using (MySqlConnection conn = new MySqlConnection(_connStr))
        {
            conn.Open();
            string sql = @"SELECT * FROM category WHERE categoryId = @categoryId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            MySqlDataReader rd06 = cmd.ExecuteReader();
            if (rd06.HasRows)
            {
                rd06.Read();
                category.CategoryId = Convert.ToInt32(rd06["CategoryId"]);
                category.CategoryName = rd06["CategoryName"].ToString();
            }
            else
            {
                throw new Exception("No record found");
            }
        }
        return category;
    }

    public Category InsertCategory(Category category)
    {
        using (MySqlConnection conn = new MySqlConnection(_connStr))
        {
            conn.Open();
            string sql = @"INSERT INTO category (CategoryName) VALUES (@CategoryName); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try{
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                int categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                category.CategoryId = categoryId;
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    public Category UpdateCategory(Category category)
    {
        using(MySqlConnection conn = new MySqlConnection(_connStr))
        {
            conn.Open();
            string sql = @"UPDATE category SET categoryName = @CategoryName WHERE categoryId = @CategoryId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                int result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("No record found");
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    public void DeleteCategory(int categoryId)
    {
        using(MySqlConnection conn = new MySqlConnection(_connStr))
        {
            conn.Open();
            string sql = @"DELETE FROM category WHERE categoryId = @CategoryId";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                int result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("No record found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

}
