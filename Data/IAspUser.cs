using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface IAspUser
{
    IEnumerable<AspUser> GetAllUsers();
    AspUser RegisterUser(AspUser user);
    AspUser GetUserByUsername(string username);
    AspUser UpdateUser(AspUser user);
    void DeleteUser(string username);
    bool Login(string username, string password);

    string GenerateToken(string username);
}
