using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Constants;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DbSeeder(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        await _context.Database.MigrateAsync();
        await SeedPermissionsAsync();
        await SeedRolesAsync();
        await SeedAdminUserAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedPermissionsAsync()
    {
        foreach (var permissionName in PermissionConstants.All)
        {
            if (!await _context.Permissions.AnyAsync(p => p.Name == permissionName))
            {
                await _context.Permissions.AddAsync(new Permission { Name = permissionName });
            }
        }
    }

    private async Task SeedRolesAsync()
    {
        if (!await _context.Roles.AnyAsync(r => r.Name == "Admin"))
        {
            var adminRole = new Role { Name = "Admin" };
            await _context.Roles.AddAsync(adminRole);
            await _context.SaveChangesAsync();

            var allPermissions = await _context.Permissions.ToListAsync();
            foreach (var permission in allPermissions)
            {
                await _context.RolePermissions.AddAsync(new RolePermission
                {
                    RoleId = adminRole.Id,
                    PermissionId = permission.Id
                });
            }
        }

        if (!await _context.Roles.AnyAsync(r => r.Name == "User"))
        {
            await _context.Roles.AddAsync(new Role { Name = "User" });
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (!await _context.Users.AnyAsync(u => u.Email == "admin@taskflow.com"))
        {
            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@taskflow.com",
                PasswordHash = _passwordHasher.Hash("Admin@123")
            };

            await _context.Users.AddAsync(admin);
            await _context.SaveChangesAsync();

            var adminRole = await _context.Roles.FirstAsync(r => r.Name == "Admin");
            await _context.UserRoles.AddAsync(new UserRole
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            });
        }
    }
}
