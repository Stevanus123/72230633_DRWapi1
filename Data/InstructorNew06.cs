using System;
using MySql.Data.MySqlClient;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class InstructorNew06 : IInstructor
{
    private readonly IConfiguration _configuration06;
    private readonly string _conn06 = string.Empty;

    public InstructorNew06(IConfiguration configuration)
    {
        _configuration06 = configuration;
        _conn06 = _configuration06.GetConnectionString("DefaultConnection");
    }

    // ini murni buatan sendiri ya kak
    public Instructor GetInstructorById(int instructorId)
    {
        Instructor instructor06 = new Instructor();
        using (MySqlConnection conn06 = new MySqlConnection(_conn06))
        {
            conn06.Open();
            string sql06 = "SELECT * FROM instructor72230633 WHERE InstructorId = @InstructorId";
            MySqlCommand cmd06 = new MySqlCommand(sql06, conn06);
            cmd06.Parameters.AddWithValue("@InstructorId", instructorId);
            MySqlDataReader rd06 = cmd06.ExecuteReader();
            if(rd06.HasRows)
            {
                rd06.Read();
                instructor06.InstructorIdku = rd06.GetInt32("InstructorId");
                instructor06.InstructorNameku = rd06.GetString("InstructorName");
                instructor06.InstructorEmailku = rd06.GetString("InstructorEmail");
                instructor06.InstructorPhoneku = rd06.GetString("InstructorPhone");
                instructor06.InstructorAddressku = rd06.GetString("InstructorAddress");
                instructor06.InstructorCityku = rd06.GetString("InstructorCity");
            }
            else
            {
                throw new Exception("Gak ada datanya, sorry yeee, gas gas mana gas!");
            }
        }
        return instructor06;
    }

    public List<Instructor> GetInstructors()
    {
        List<Instructor> instructors06 = new List<Instructor>();
        using (MySqlConnection conn06 = new MySqlConnection(_conn06))
        {
            conn06.Open();
            string sql06 = "SELECT * FROM instructor72230633";
            MySqlCommand cmd06 = new MySqlCommand(sql06, conn06);
            MySqlDataReader rd06 = cmd06.ExecuteReader();
            while (rd06.Read())
            {
                Instructor instructor = new Instructor();
                instructor.InstructorIdku = rd06.GetInt32("InstructorId");
                instructor.InstructorNameku = rd06.GetString("InstructorName");
                instructor.InstructorEmailku = rd06.GetString("InstructorEmail");
                instructor.InstructorPhoneku = rd06.GetString("InstructorPhone");
                instructor.InstructorAddressku = rd06.GetString("InstructorAddress");
                instructor.InstructorCityku = rd06.GetString("InstructorCity");
                instructors06.Add(instructor);
            }
            rd06.Close();
            cmd06.Dispose();
        }
        return instructors06;
    }

    public Instructor InsertInstructor(Instructor instructor)
    {
        using(MySqlConnection conn06 = new MySqlConnection(_conn06))
        {
            conn06.Open();
            string sql06 = @"INSERT INTO instructor72230633 (InstructorName, InstructorEmail, InstructorPhone, InstructorAddress, InstructorCity) VALUES (@InstructorName, @InstructorEmail, @InstructorPhone, @InstructorAddress, @InstructorCity); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd06 = new MySqlCommand(sql06, conn06);
            try
            {
                cmd06.Parameters.AddWithValue("@InstructorName", instructor.InstructorNameku);
                cmd06.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmailku);
                cmd06.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhoneku);
                cmd06.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddressku);
                cmd06.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCityku);
                int instructorId = Convert.ToInt32(cmd06.ExecuteScalar());
                instructor.InstructorIdku = instructorId;
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Iki lo error e mergo: "+ex.Message);
            }
            finally
            {
                cmd06.Dispose();
                conn06.Close();
            }
        }
    }

    public Instructor UpdateInstructor(Instructor instructor)
    {
        using(MySqlConnection conn06 = new MySqlConnection(_conn06))
        {
            conn06.Open();
            string sql06 = @"UPDATE instructor72230633 SET InstructorName = @InstructorName, InstructorEmail = @InstructorEmail, InstructorPhone = @InstructorPhone, InstructorAddress = @InstructorAddress, InstructorCity = @InstructorCity WHERE InstructorId = @InstructorId";
            MySqlCommand cmd06 = new MySqlCommand(sql06, conn06);
            try
            {
                cmd06.Parameters.AddWithValue("@InstructorName", instructor.InstructorNameku);
                cmd06.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmailku);
                cmd06.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhoneku);
                cmd06.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddressku);
                cmd06.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCityku);
                cmd06.Parameters.AddWithValue("@InstructorId", instructor.InstructorIdku);
                int asile_nggih = cmd06.ExecuteNonQuery();
                if (asile_nggih == 0)
                {
                    throw new Exception("Gak bisa masuk datane lur, piye ee?");
                }
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error maneh yo, mumet? iki woconen error e: "+ex.Message);
            }
            finally
            {
                cmd06.Dispose();
                conn06.Close();
            }
        }
    }
    public void DeleteInstructor(int instructorId)
    {
        using(MySqlConnection conn06 = new MySqlConnection(_conn06))
        {
            conn06.Open();
            string sql06 = @"DELETE FROM instructor72230633 WHERE InstructorId = @InstructorId";
            MySqlCommand cmd06 = new MySqlCommand(sql06, conn06);
            try
            {
                cmd06.Parameters.AddWithValue("@InstructorId", instructorId);
                int isa_dihapus_ra = cmd06.ExecuteNonQuery();
                if (isa_dihapus_ra == 0)
                {
                    throw new Exception("Mumet iki ra kena dihapus");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gak bisa diapus lur, piye ee? iki namatno error e: "+ex.Message);
            }
            finally
            {
                cmd06.Dispose();
                conn06.Close();
            }
        }
    }
}
