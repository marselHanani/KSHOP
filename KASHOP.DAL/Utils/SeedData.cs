using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager
            ,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Models.Category { Name = "Electronics", CreatedAt = DateTime.Now, Status = Models.Status.Active },
                    new Models.Category { Name = "Clothing", CreatedAt = DateTime.Now, Status = Models.Status.Active },
                    new Models.Category { Name = "Home Appliances", CreatedAt = DateTime.Now, Status = Models.Status.Active }
                );
                await _context.SaveChangesAsync();
            }
            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Models.Brand { Name = "Samsung", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "3a470a9a-ee2a-4a95-bb8a-f020a4dcbe0b.jpg"},
                    new Models.Brand { Name = "Apple", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "92bd52bd-9230-4fe8-b8e0-32d8c9b7bb1d.jpg"},
                    new Models.Brand { Name = "Nike", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "fb41a151-3490-4220-9923-55ad3028d27b.jpg"}
                );
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (!await _userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "Marsel",
                    Email = "Marsel@gmail.com",
                    PhoneNumber = "994123456789",
                };
                var result = await _userManager.CreateAsync(adminUser, "Pass@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // Log or handle errors here
                    // result.Errors contains the details
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
