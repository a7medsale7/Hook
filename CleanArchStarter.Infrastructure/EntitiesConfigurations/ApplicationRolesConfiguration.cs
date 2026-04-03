using CleanArchStarter.Domain.Consts;
using CleanArchStarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Infrastructure.EntitiesConfigurations;
public class ApplicationRolesConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData([
            new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
            },

             new ApplicationRole
            {
                Id = DefaultRoles.UserRoleId,
                Name = DefaultRoles.User,
                NormalizedName = DefaultRoles.User.ToUpper(),
                ConcurrencyStamp = DefaultRoles.UserRoleConcurrencyStamp,
                IsDefault = true,
            },
            ]);
    }
}
