using System;
using Microsoft.AspNetCore.Identity;

namespace Hook.HashGen;

public class ApplicationUser : IdentityUser {}

class Program
{
    static void Main()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser { UserName = "complaintadmin@hook.com" };
        var hash = hasher.HashPassword(user, "Admin@Hook123");
        Console.WriteLine("HASH:" + hash);
    }
}
