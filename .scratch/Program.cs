using System;
using Microsoft.AspNetCore.Identity;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasher = new PasswordHasher<object>();
            var hash = hasher.HashPassword(null, "Mos@ma246810");
            Console.WriteLine($"HASH: {hash}");
        }
    }
}
