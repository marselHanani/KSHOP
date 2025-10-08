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
                    new Models.Brand { Name = "Samsung", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "adidas.png"},
                    new Models.Brand { Name = "Apple", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "Nike.png"},
                    new Models.Brand { Name = "Nike", CreatedAt = DateTime.Now, Status = Models.Status.Active,ImageUrl = "puma.png"}
                );
                await _context.SaveChangesAsync();
            }

            if (!await _context.Products.AnyAsync())
            {
                var electronicsCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Electronics");
                var clothingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Clothing");
                var homeAppliancesCategory =
                    await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Home Appliances");

                var samsungBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == "Samsung");
                var appleBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == "Apple");
                var nikeBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == "Nike");

                await _context.Products.AddRangeAsync(
                    new Product
                    {
                        Name = "Adidas Shoos",
                        Description = "Latest Samsung smartphone with advanced camera features",
                        Price = 999.99m,
                        Discount = 0m,
                        MainImage = "adidas_sh.jpeg",
                        Quantity = 50,
                        Rate = 4.7,
                        CategoryId = electronicsCategory?.Id,
                        BrandId = samsungBrand?.Id,
                        CreatedAt = DateTime.Now,
                        Status = Status.Active
                    },
                    new Product
                    {
                        Name = "iPhone 15 Pro",
                        Description = "Powerful Apple smartphone with A17 chip",
                        Price = 1099.99m,
                        Discount = 50m,
                        MainImage = "nike_sh.jpeg",
                        Quantity = 30,
                        Rate = 4.8,
                        CategoryId = electronicsCategory?.Id,
                        BrandId = appleBrand?.Id,
                        CreatedAt = DateTime.Now,
                        Status = Status.Active
                    },
                    new Product
                    {
                        Name = "Nike Air Max",
                        Description = "Comfortable running shoes with air cushioning",
                        Price = 129.99m,
                        Discount = 20m,
                        MainImage = "puma_sh.jpeg",
                        Quantity = 100,
                        Rate = 4.5,
                        CategoryId = clothingCategory?.Id,
                        BrandId = nikeBrand?.Id,
                        CreatedAt = DateTime.Now,
                        Status = Status.Active
                    }
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
