using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;

namespace WebApplication1.Service;

public class TokenService
{
    private readonly AspUserEF _aspUserEF;
    public TokenService(AspUserEF aspUserEF)
    {
        _aspUserEF = aspUserEF;
    }

}
