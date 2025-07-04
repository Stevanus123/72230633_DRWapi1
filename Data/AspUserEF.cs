using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AspUserEF : IAspUser
{
    private readonly ApplicationDbContext _context;
    public AspUserEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public void DeleteUser(string username)
    {
        var user = GetUserByUsername(username);
        if (user == null)
            throw new Exception("Data gak ditemukan!");
        try
        {
            _context.AspUsers.Remove(user);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Gagal dihapus karena: ", ex);
        }
    }

    public string GenerateToken(string username)
    {
        var user = GetUserByUsername(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var key = Helpers.ApiSettings.GenerateSecretByte();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public IEnumerable<AspUser> GetAllUsers()
    {
        var users = _context.AspUsers.ToList();
        return users;
    }

    public AspUser GetUserByUsername(string username)
    {
        var user = _context.AspUsers.FirstOrDefault(c => c.Username == username);
        if (user == null)
            throw new Exception("Gak ditemukan wkwkwkk!");
        return user;
    }

    public bool Login(string username, string password)
    {
        var user = _context.AspUsers.FirstOrDefault(c => c.Username == username);
        if (user == null)
            return false;
        var pass = Helpers.HashHelper.HashPassword(password);
        if (user.Password == pass)
            return true;
        else
            return false;
    }

    public AspUser RegisterUser(AspUser user)
    {
        try
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User gaboleh kosong dong!");

            user.Password = Helpers.HashHelper.HashPassword(user.Password);
            _context.AspUsers.Add(user);
            _context.SaveChanges();
            return user;
        }
        catch (DbUpdateException dbex)
        {
            throw new Exception("Error karena: ", dbex);
        }
        catch (System.Exception ex)
        {
            throw new Exception("Error karena: ", ex);
        }
    }

    public AspUser UpdateUser(AspUser user)
    {
        var existingUser = GetUserByUsername(user.Username);
        if (existingUser == null)
            throw new Exception($"username {user.Username} gak ditemukan!");
        try
        {
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Address = user.Address;
            existingUser.City = user.City;
            existingUser.Country = user.Country;
            _context.AspUsers.Update(existingUser);
            _context.SaveChanges();
            return existingUser;
        }
        catch (Exception ex)
        {
            throw new Exception("Gagal mengubah data karena: ", ex);
        }
    }
}
