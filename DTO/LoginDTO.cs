using System;

namespace WebApplication1.DTO;

public class LoginDTO
{
public string Username { get; set; } = null!;
public string Password { get; set; } = null!;
public string? Token { get; set; }
}
