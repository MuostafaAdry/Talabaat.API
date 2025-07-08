using Domain.Contracts;
using Domain.Models.Identity;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DataSeed(ApplicationDbContext _dbContext,
       UserManager<ApplicationUser> _userManager,
       RoleManager<IdentityRole> _roleManager, ApplicationIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public void DataSeeding()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Presistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Presistence\Data\DataSeed\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Presistence\Data\DataSeed\Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (products != null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }
                }

                if (!_dbContext.DelivaryMethods.Any())
                {
                    var DelivaryData = File.ReadAllText(@"..\Presistence\Data\DataSeed\delivery.json");
                    var Delivary = JsonSerializer.Deserialize<List<DelivaryMethod>>(DelivaryData);
                    if (Delivary != null && Delivary.Any())
                    {
                        _dbContext.DelivaryMethods.AddRange(Delivary);
                    }
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding: {ex.Message}");
            }

        }

        public async Task IdentityDataSeed()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    //add roles
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    // add users
                    var user1 = new ApplicationUser()
                    {
                        DisplayName = "MostafaAdry",
                        Email = "mostafaelshere256@mail.com",
                        PhoneNumber = "123456789",
                        UserName = "Mostafa"
                    };
                    var user2 = new ApplicationUser()
                    {
                        DisplayName = " Adry",
                        Email = "adry10@mail.com",
                        PhoneNumber = "123456789",
                        UserName = "Adry"
                    };

                    // add password
                    await _userManager.CreateAsync(user1, "P@$$w0rd");
                    await _userManager.CreateAsync(user2, "P@$$w0rd");
                    // custom role to eacg user
                    await _userManager.AddToRoleAsync(user1, "Admin");
                    await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                    await _identityDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
