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
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
            Permissions.BoatOwner_Apply, // يقدر يقدم طلب عشان يبقى صاحب مركب
            Permissions.BoatOwner_ViewProfile,
            Permissions.Boats_View // يقدر يتصفح المراكب عشان يحجز
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
            Permissions.Boats_Delete
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

        builder.HasData(allClaims);
    }
}
