using Hook.Domain.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Hook.Infrastructure.EntitiesConfigurations;

public class ApplicationRoleClaimConfigurations : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var allPermissions = Permissions.GetAllPermissions();
        var allClaims = new List<IdentityRoleClaim<string>>();
        int claimId = 1;

        // 1. Admin Permissions (بياخد كل الصلاحيات أوتوماتيكياً)
        foreach (var permission in allPermissions)
        {
            if (string.IsNullOrEmpty(permission)) continue;
            allClaims.Add(new IdentityRoleClaim<string>
            {
                Id = claimId++,
                RoleId = DefaultRoles.AdminRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // 2. User Permissions (صلاحيات المستخدم العادي / المستأجر)
        var userPermissions = new List<string>
        {
            //trips related
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.BoatOwner_Apply, // يقدر يقدم طلب عشان يبقى صاحب مركب
            Permissions.BoatOwner_ViewProfile,
            Permissions.Boats_View, // يقدر يتصفح المراكب عشان يحجز
            Permissions.Trips_View, // يقدر يشوف الرحلات المتاحة
            Permissions.Bookings_View,
            Permissions.Bookings_Create,
            Permissions.Bookings_Cancel,
            Permissions.Payments_View,
            Permissions.Payments_UploadReceipt,
            Permissions.Reviews_View,
            Permissions.Reviews_Create,
            Permissions.Reviews_Update,
            Permissions.Reviews_Delete,

            //Marketplace related
            Permissions.Seller_Apply, //he can apply to be a seller
            Permissions.Seller_ViewProfile,

             // Marketplace (Buyer)
            Permissions.MarketplaceProducts_View,
            Permissions.MarketplaceCart_View,
            Permissions.MarketplaceCart_Update,
            Permissions.MarketplaceOrders_View,
            Permissions.MarketplaceOrders_Create,
            Permissions.MarketplaceOrders_Cancel,
            Permissions.MarketplaceOrders_UpdateStatus, // confirm received after OutForDelivery
            Permissions.MarketplaceReviews_View,
            Permissions.MarketplaceReviews_Create

        };

        foreach (var permission in userPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                Id = claimId++,
                RoleId = DefaultRoles.UserRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // 3. BoatOwner Permissions (صلاحيات صاحب المركب)
        var boatOwnerPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.BoatOwner_ViewProfile,
            // صلاحيات إدارة المراكب الخاصة به
            Permissions.Boats_View,
            Permissions.Boats_Create,
            Permissions.Boats_Update,
            Permissions.Boats_Delete,
            // صلاحيات إدارة الرحلات الخاصة به
            Permissions.Trips_View,
            Permissions.Trips_Create,
            Permissions.Trips_Update,
            Permissions.Trips_Delete,
            // صلاحيات إدارة الحجوزات الخاصة به
            Permissions.Bookings_View,
            Permissions.Bookings_ViewAll,
            Permissions.Bookings_UpdateStatus,
            Permissions.Payments_View,
            Permissions.Payments_Verify,
            Permissions.Payments_Stats,
            Permissions.Reviews_View
        };

        foreach (var permission in boatOwnerPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                Id = claimId++,
                RoleId = DefaultRoles.BoatOwnerRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        // 4. Seller Permissions (Marketplace Seller)
        var sellerPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.Seller_ViewProfile,

            // Marketplace (Seller)
            Permissions.MarketplaceProducts_View,
            Permissions.MarketplaceProducts_Create,
            Permissions.MarketplaceProducts_Update,
            Permissions.MarketplaceProducts_Delete,

            Permissions.MarketplaceOrders_View,
            Permissions.MarketplaceOrders_UpdateStatus,
            Permissions.MarketplaceOrders_Stats,

            Permissions.MarketplaceReviews_View
        };

        foreach (var permission in sellerPermissions)
        {
            allClaims.Add(new IdentityRoleClaim<string>
            {
                Id = claimId++,
                RoleId = DefaultRoles.SellerRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        builder.HasData(allClaims);
    }
}
