using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace ContactManager.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {              
                var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                SeedIdentity(UserManager, context);
                SeedDB(context, "0");
            }
        }        

        public static void SeedIdentity(UserManager<IdentityUser> UserManager, ApplicationDbContext context)
        {
            Console.WriteLine("Seeding identity");
            string email = "a@a.com";
            string password = "stanislav";
            var user = new IdentityUser { UserName = email, Email = email };
            var result = UserManager.CreateAsync(user, password);
            // If the new user is a duplicate we'll get a warning
            // and the user wont be created
            if(result.IsCompletedSuccessfully)
            {
                Console.WriteLine("Created user.");
            }
            else 
            {
                Console.WriteLine($"Failed to create user, {result}");
            }

        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            context.Contact.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "debra@example.com"
                },
                new Contact
                {
                    Name = "Thorsten Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "thorsten@example.com"
                },
             new Contact
             {
                 Name = "Yuhong Li",
                 Address = "9012 State st",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "yuhong@example.com"
             },
             new Contact
             {
                 Name = "Jon Orton",
                 Address = "3456 Maple St",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "jon@example.com"
             },
             new Contact
             {
                 Name = "Diliana Alexieva-Bosseva",
                 Address = "7890 2nd Ave E",
                 City = "Redmond",
                 State = "WA",
                 Zip = "10999",
                 Email = "diliana@example.com"
             }
             );
            context.SaveChanges();
        }

    }
}