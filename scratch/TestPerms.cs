using System;
using System.Linq;
using Hook.Domain.Consts;

namespace TestPermissions
{
    class Program
    {
        static void Main()
        {
            var permissions = Permissions.GetAllPermissions();
            Console.WriteLine($"Total Permissions: {permissions.Count}");
            var deletePermission = permissions.FirstOrDefault(p => p == "Permissions.Bookings.Delete");
            Console.WriteLine($"Found Bookings.Delete: {deletePermission != null}");
            
            foreach(var p in permissions.OrderBy(p => p))
            {
                Console.WriteLine(p);
            }
        }
    }
}
