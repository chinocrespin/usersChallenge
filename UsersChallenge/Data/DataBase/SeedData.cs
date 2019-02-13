using System;
using System.Collections.Generic;
using System.Text;
using Identity.Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public class SeedData
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            DbContextOptions<MyDbContext> ortWorkshopContext = serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>();

            using (var dbContext = new MyDbContext(ortWorkshopContext))
            {
                //Ensure database is created
                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    var usersService = (IUsersService) serviceProvider.GetService(typeof(IUsersService));
                    var users = usersService.GetRandomUsers()?.Result;
                    if (users != null)
                    {
                        dbContext.Users.AddRange(users);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
