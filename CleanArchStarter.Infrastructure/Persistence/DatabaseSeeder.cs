using Hook.Domain.Consts;
using Hook.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // 1. Seed Roles
        var roles = new List<ApplicationRole>
        {
            new() { Id = DefaultRoles.AdminRoleId, Name = DefaultRoles.Admin, NormalizedName = DefaultRoles.Admin.ToUpper(), ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp },
            new() { Id = DefaultRoles.UserRoleId, Name = DefaultRoles.User, NormalizedName = DefaultRoles.User.ToUpper(), ConcurrencyStamp = DefaultRoles.UserRoleConcurrencyStamp, IsDefault = true },
            new() { Id = DefaultRoles.BoatOwnerRoleId, Name = DefaultRoles.BoatOwner, NormalizedName = DefaultRoles.BoatOwner.ToUpper(), ConcurrencyStamp = DefaultRoles.BoatOwnerConcurrencyStamp },
            new() { Id = DefaultRoles.SellerRoleId, Name = DefaultRoles.Seller, NormalizedName = DefaultRoles.Seller.ToUpper(), ConcurrencyStamp = DefaultRoles.SellerRoleConcurrencyStamp },
            new() { Id = DefaultRoles.CommunityAdminRoleId, Name = DefaultRoles.CommunityAdmin, NormalizedName = DefaultRoles.CommunityAdmin.ToUpper(), ConcurrencyStamp = DefaultRoles.CommunityAdminRoleConcurrencyStamp }
        };

        foreach (var role in roles)
        {
            if (!await context.Roles.AnyAsync(r => r.Id == role.Id))
            {
                context.Roles.Add(role);
            }
        }
        await context.SaveChangesAsync();

        // 2. Seed Role Claims (Permissions)
        var seededRoleIds = roles.Select(r => r.Id).ToList();
        var existingClaims = await context.Set<IdentityRoleClaim<string>>().Where(rc => seededRoleIds.Contains(rc.RoleId)).ToListAsync();
        context.Set<IdentityRoleClaim<string>>().RemoveRange(existingClaims);
        await context.SaveChangesAsync();

        var allPermissions = Permissions.GetAllPermissions();
        var allClaims = new List<IdentityRoleClaim<string>>();

        // Admin Permissions (All permissions)
        foreach (var permission in allPermissions)
        {
            if (string.IsNullOrEmpty(permission)) continue;
            allClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = DefaultRoles.AdminRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // User Permissions
        var userPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.BoatOwner_Apply,
            Permissions.BoatOwner_ViewProfile,
            Permissions.Boats_View,
            Permissions.Trips_View,
            Permissions.Bookings_View,
            Permissions.Bookings_Create,
            Permissions.Bookings_Cancel,
            Permissions.Payments_View,
            Permissions.Payments_UploadReceipt,
            Permissions.Reviews_View,
            Permissions.Reviews_Create,
            Permissions.Reviews_Update,
            Permissions.Reviews_Delete,
            Permissions.Seller_Apply,
            Permissions.Seller_ViewProfile,
            Permissions.MarketplaceProducts_View,
            Permissions.MarketplaceCart_View,
            Permissions.MarketplaceCart_Update,
            Permissions.MarketplaceOrders_View,
            Permissions.MarketplaceOrders_Create,
            Permissions.MarketplaceOrders_Cancel,
            Permissions.MarketplaceOrders_UpdateStatus,
            Permissions.MarketplaceReviews_View,
            Permissions.MarketplaceReviews_Create,
            Permissions.Community_Posts_Create,
            Permissions.Community_Posts_Update,
            Permissions.Community_Posts_Delete,
            Permissions.Community_Posts_Like,
            Permissions.Community_Posts_Save,
            Permissions.Community_Posts_Report,
            Permissions.Community_Comments_Add,
            Permissions.Community_Comments_Delete,
            Permissions.Community_User_Follow,
            Permissions.Community_Events_Join,
            Permissions.Community_Events_Participants_View,
            Permissions.Community_Feed_View,
            Permissions.Community_Notifications_View,
            Permissions.Community_Complaints_Support
        };

        foreach (var permission in userPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = DefaultRoles.UserRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // BoatOwner Permissions
        var boatOwnerPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.BoatOwner_ViewProfile,
            Permissions.Boats_View,
            Permissions.Boats_Create,
            Permissions.Boats_Update,
            Permissions.Boats_Delete,
            Permissions.Trips_View,
            Permissions.Trips_Create,
            Permissions.Trips_Update,
            Permissions.Trips_Delete,
            Permissions.Bookings_View,
            Permissions.Bookings_ViewAll,
            Permissions.Bookings_UpdateStatus,
            Permissions.Payments_View,
            Permissions.Payments_Verify,
            Permissions.Payments_Stats,
            Permissions.Reviews_View,
            Permissions.Community_Posts_Create,
            Permissions.Community_Posts_Update,
            Permissions.Community_Posts_Delete,
            Permissions.Community_Posts_Like,
            Permissions.Community_Posts_Save,
            Permissions.Community_Posts_Report,
            Permissions.Community_Comments_Add,
            Permissions.Community_Comments_Delete,
            Permissions.Community_User_Follow,
            Permissions.Community_Events_Join,
            Permissions.Community_Events_Participants_View,
            Permissions.Community_Feed_View,
            Permissions.Community_Notifications_View,
            Permissions.Community_Complaints_Support
        };

        foreach (var permission in boatOwnerPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = DefaultRoles.BoatOwnerRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // Seller Permissions
        var sellerPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.Seller_ViewProfile,
            Permissions.MarketplaceProducts_View,
            Permissions.MarketplaceProducts_Create,
            Permissions.MarketplaceProducts_Update,
            Permissions.MarketplaceProducts_Delete,
            Permissions.MarketplaceOrders_View,
            Permissions.MarketplaceOrders_UpdateStatus,
            Permissions.MarketplaceOrders_Stats,
            Permissions.MarketplaceReviews_View,
            Permissions.Community_Posts_Create,
            Permissions.Community_Posts_Update,
            Permissions.Community_Posts_Delete,
            Permissions.Community_Posts_Like,
            Permissions.Community_Posts_Save,
            Permissions.Community_Posts_Report,
            Permissions.Community_Comments_Add,
            Permissions.Community_Comments_Delete,
            Permissions.Community_User_Follow,
            Permissions.Community_Events_Join,
            Permissions.Community_Events_Participants_View,
            Permissions.Community_Feed_View,
            Permissions.Community_Notifications_View,
            Permissions.Community_Complaints_Support
        };

        foreach (var permission in sellerPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = DefaultRoles.SellerRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // CommunityAdmin Permissions
        var communityAdminPermissions = new List<string>
        {
            Permissions.CommunityAdmin_Complaints_View,
            Permissions.CommunityAdmin_Complaints_Resolve,
            Permissions.Community_Notifications_View,
            Permissions.FishGuardAdmin_Manage
        };

        foreach (var permission in communityAdminPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = DefaultRoles.CommunityAdminRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        context.Set<IdentityRoleClaim<string>>().AddRange(allClaims);
        await context.SaveChangesAsync();

        // 3. Seed Default Users
        var defaultUsers = new List<ApplicationUser>
        {
            new()
            {
                Id = DefaultUsers.AdminId,
                FirstName = "Admin",
                LastName = "Account",
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                UserName = DefaultUsers.AdminEmail,
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.AdminPasswordHash
            },
            new()
            {
                Id = DefaultUsers.ComplaintAdminId,
                FirstName = "Complaint",
                LastName = "Admin",
                Email = DefaultUsers.ComplaintAdminEmail,
                NormalizedEmail = DefaultUsers.ComplaintAdminEmail.ToUpper(),
                UserName = DefaultUsers.ComplaintAdminEmail,
                NormalizedUserName = DefaultUsers.ComplaintAdminEmail.ToUpper(),
                SecurityStamp = DefaultUsers.ComplaintAdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.ComplaintAdminConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.ComplaintAdminPasswordHash
            }
        };

        foreach (var user in defaultUsers)
        {
            if (!await context.Users.AnyAsync(u => u.Id == user.Id))
            {
                context.Users.Add(user);
            }
        }
        await context.SaveChangesAsync();

        // 4. Seed User Roles
        var userRoles = new List<IdentityUserRole<string>>
        {
            new() { UserId = DefaultUsers.AdminId, RoleId = DefaultRoles.AdminRoleId },
            new() { UserId = DefaultUsers.ComplaintAdminId, RoleId = DefaultRoles.CommunityAdminRoleId }
        };

        foreach (var ur in userRoles)
        {
            if (!await context.Set<IdentityUserRole<string>>().AnyAsync(u => u.UserId == ur.UserId && u.RoleId == ur.RoleId))
            {
                context.Set<IdentityUserRole<string>>().Add(ur);
            }
        }
        await context.SaveChangesAsync();

        // 5. Seed Locations if empty
        if (!await context.FishingLocations.AnyAsync())
        {
            var locations = new List<FishingLocation>
            {
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "القاهرة", Governorate = "Cairo", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "الإسكندرية", Governorate = "Alexandria", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "الجيزة", Governorate = "Giza", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "بورسعيد", Governorate = "Port Said", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "السويس", Governorate = "Suez", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "دمياط", Governorate = "Damietta", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "الدقهلية", Governorate = "Dakahlia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "الشرقية", Governorate = "Sharkia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "الغربية", Governorate = "Gharbia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "كفر الشيخ", Governorate = "Kafr El Sheikh", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "المنوفية", Governorate = "Monufia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "البحيرة", Governorate = "Beheira", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "القليوبية", Governorate = "Qalyubia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "الإسماعيلية", Governorate = "Ismailia", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "الفيوم", Governorate = "Faiyum", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000016"), Name = "بني سويف", Governorate = "Beni Suef", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000017"), Name = "المنيا", Governorate = "Minya", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000018"), Name = "أسيوط", Governorate = "Assiut", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000019"), Name = "سوهاج", Governorate = "Sohag", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000020"), Name = "قنا", Governorate = "Qena", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000021"), Name = "الأقصر", Governorate = "Luxor", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000022"), Name = "أسوان", Governorate = "Aswan", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000023"), Name = "البحر الأحمر", Governorate = "Red Sea", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000024"), Name = "الوادي الجديد", Governorate = "New Valley", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000025"), Name = "مطروح", Governorate = "Matrouh", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000026"), Name = "شمال سيناء", Governorate = "North Sinai", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000027"), Name = "جنوب سيناء", Governorate = "South Sinai", CreatedById = "System", CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            };
            context.FishingLocations.AddRange(locations);
            await context.SaveChangesAsync();
        }
    }
}
