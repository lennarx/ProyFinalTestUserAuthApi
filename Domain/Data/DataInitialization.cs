using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Data
{
    public static class DataInitialization
    {
        public static void Initiate(UsersContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Name = "Franco Redoni",
                    Email = "franco.redoni@gmail.com",
                    IsActive = true,
                    Password = Encoding.ASCII.GetBytes("Test1234")
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
