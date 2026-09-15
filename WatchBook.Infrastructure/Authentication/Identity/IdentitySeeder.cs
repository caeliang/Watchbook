using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace WatchBook.Infrastructure.Identity;

/// <summary>
/// Helper methods to seed identity data such as roles and a default administrator.
/// Designed to be invoked during application startup.
/// </summary>
public static class IdentitySeeder
{
    /// <summary>
    /// Ensures the specified roles exist in the role store.
    /// </summary>
    /// <param name="roleManager">The <see cref="RoleManager{ApplicationRole}"/>.</param>
    /// <param name="roles">Collection of role names to ensure.</param>
    public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager, IEnumerable<string> roles)
    {
        ArgumentNullException.ThrowIfNull(roleManager);
        ArgumentNullException.ThrowIfNull(roles);
        foreach (var roleName in roles)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                continue;

            var exists = await roleManager.RoleExistsAsync(roleName).ConfigureAwait(false);
            if (!exists)
            {
                var role = new ApplicationRole { Name = roleName };
                var result = await roleManager.CreateAsync(role).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create role '{roleName}': {errors}");
                }
            }
        }
    }

    /// <summary>
    /// Ensures a default administrator user exists and is assigned to the Administrator role.
    /// If adminPassword is null or empty the method will not create the admin user.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{ApplicationUser}"/>.</param>
    /// <param name="roleManager">The <see cref="RoleManager{ApplicationRole}"/>.</param>
    /// <param name="adminEmail">The administrator email.</param>
    /// <param name="adminPassword">The administrator password.</param>
    public static async Task SeedDefaultAdminAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        string? adminEmail,
        string? adminPassword)
    {
        ArgumentNullException.ThrowIfNull(userManager);
        ArgumentNullException.ThrowIfNull(roleManager);
        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            // Do not create admin when credentials are not provided; roles are still seeded separately.
            return;
        }

        var adminUser = await userManager.FindByEmailAsync(adminEmail).ConfigureAwait(false);
        if (adminUser != null)
        {
            // Ensure admin is in Administrator role
            if (!await userManager.IsInRoleAsync(adminUser, Roles.Administrator).ConfigureAwait(false))
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Administrator).ConfigureAwait(false);
            }

            return;
        }

        // Create new admin user
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            DisplayName = "Administrator",
            CreatedAt = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(adminUser, adminPassword).ConfigureAwait(false);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create admin user '{adminEmail}': {errors}");
        }

        var addToRoleResult = await userManager.AddToRoleAsync(adminUser, Roles.Administrator).ConfigureAwait(false);
        if (!addToRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to add admin user to role '{Roles.Administrator}': {errors}");
        }
    }
}
