using CleanArchStarter.Domain.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Infrastructure.EntitiesConfigurations;
public class ApplicationRoleClaimConfigurations : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var allPermissions = Permissions.GetAllPermissions();
        var allClaims = new List<IdentityRoleClaim<string>>();
        int claimId = 1;

        // 1. Admin Permissions (All)
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




        // 3. User Permissions
        var userPermissions = new List<string>
        {
            Permissions.Users_ViewProfile,
            Permissions.Users_UpdateProfile,
            Permissions.Users_ChangePassword,
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

        builder.HasData(allClaims);
    }
}
