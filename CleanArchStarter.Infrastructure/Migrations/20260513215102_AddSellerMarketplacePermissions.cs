using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchStarter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerMarketplacePermissions : Migration
    {
        // First ensure the Seller Role exists, then insert the missing permissions.
        // Using raw SQL with IF NOT EXISTS makes this migration safe and idempotent
        // regardless of whether previous HasData seeding ran on this database.
        private const string SellerRoleId = "6c6e00c1-6b2a-48b7-9d2f-3dfe9b3c0a1b";
        private const string SellerRoleName = "Seller";
        private const string ClaimType = "Permissions";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Ensure the Seller Role itself exists in AspNetRoles.
            // Check by NormalizedName too to avoid duplicate key if the role exists with a different ID.
            // If Seller role already exists (with any ID), get its real ID and use it for claims.
            migrationBuilder.Sql($@"
                IF NOT EXISTS (SELECT 1 FROM [AspNetRoles] WHERE [NormalizedName] = 'SELLER')
                BEGIN
                    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [IsDefault])
                    VALUES ('{SellerRoleId}', '{SellerRoleName}', 'SELLER', 'c2b1d0e9-6e7d-4d8f-9a3b-1f2e3d4c5b6a', 0)
                END
            ");

            // Step 2: Insert the missing Seller Marketplace permissions.
            // Uses the actual RoleId from the DB (in case it differs from the seeded constant).
            // Using IF NOT EXISTS so this migration is safe to re-run (idempotent).
            var missingPermissions = new[]
            {
                "Permissions.Marketplace.Products.Create",
                "Permissions.Marketplace.Products.Update",
                "Permissions.Marketplace.Products.Delete",
                "Permissions.Marketplace.Orders.Stats",
            };

            foreach (var permission in missingPermissions)
            {
                migrationBuilder.Sql($@"
                    DECLARE @ActualSellerRoleId NVARCHAR(450);
                    SELECT @ActualSellerRoleId = [Id] FROM [AspNetRoles] WHERE [NormalizedName] = 'SELLER';

                    IF @ActualSellerRoleId IS NOT NULL AND NOT EXISTS (
                        SELECT 1 FROM [AspNetRoleClaims]
                        WHERE [RoleId] = @ActualSellerRoleId
                          AND [ClaimType] = '{ClaimType}'
                          AND [ClaimValue] = '{permission}'
                    )
                    BEGIN
                        INSERT INTO [AspNetRoleClaims] ([RoleId], [ClaimType], [ClaimValue])
                        VALUES (@ActualSellerRoleId, '{ClaimType}', '{permission}')
                    END
                ");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var permissionsToRemove = new[]
            {
                "Permissions.Marketplace.Products.Create",
                "Permissions.Marketplace.Products.Update",
                "Permissions.Marketplace.Products.Delete",
                "Permissions.Marketplace.Orders.Stats",
            };

            foreach (var permission in permissionsToRemove)
            {
                migrationBuilder.Sql($@"
                    DELETE FROM [AspNetRoleClaims]
                    WHERE [RoleId] = '{SellerRoleId}'
                      AND [ClaimType] = '{ClaimType}'
                      AND [ClaimValue] = '{permission}'
                ");
            }
        }
    }
}
