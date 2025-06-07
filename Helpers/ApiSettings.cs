using System;
using System.Text;

namespace WebApplication1.Helpers;

public class ApiSettings
{
    internal static string secretKey = "iniRahasiaSuperPanjangMinimal32Karakter!!!";
    internal static byte[] GenerateSecretByte() => Encoding.UTF8.GetBytes(secretKey);
}
