using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace WantToSell.Application.Extensions;

public static class FormFileExtensions
{
    public static async Task<string> CalculateSHA256Async(this IFormFile file)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = file.OpenReadStream())
            {
                var hash = await sha256.ComputeHashAsync(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}