using System;
using System.Collections.Generic;
using System.Text;
using Identity.Domain.Models;
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
                    dbContext.Users.AddRange(new User[]
                    {
                        new User{ IdValue="1", UserName="User 1", Location= new Location{ Street = "Street 1"} },
                        new User{ IdValue="2", UserName="User 2", Location= new Location{ Street = "Street 2"} },
                        new User{ IdValue="3", UserName="User 3", Location= new Location{ Street = "Street 3"} }
                    });
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
