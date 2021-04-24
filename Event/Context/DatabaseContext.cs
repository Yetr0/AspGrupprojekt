using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Event.Models;

namespace Event.Context
{
    public class DatabaseContext : IdentityDbContext<MyUser>
    {


 
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<MyUser> MyUsers { get; set; }

        public DbSet<Events> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Events>()
                .HasOne(e => e.Organizer)
                .WithMany(e => e.HostedEvents);
            builder.Entity<Events>()
                .HasMany(e => e.Attendees)
                .WithMany(e => e.MyEvents);
        }

        public async Task ResetAndSeedAsync(UserManager<MyUser> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            //Creates users for each role

            MyUser adminUser = new MyUser()
            {
                UserName = "admin_user",
                EmailConfirmed = true,
                Email = "admin@hotmail.com",
            };
            await userManager.CreateAsync(adminUser, "Passw0rd!");

            MyUser OrganizerUser = new MyUser()
            {
                UserName = "Organizer_User",
                EmailConfirmed = true,
                Email = "organizer@hotmail.com",
            };
            await userManager.CreateAsync(OrganizerUser, "Passw0rd!");


            MyUser User = new MyUser()
            {
                UserName = "User",
                EmailConfirmed = true,
                Email = "user@hotmail.com",
            };
            await userManager.CreateAsync(User, "Passw0rd!");


            //Creates the roles 

            string[] roles = { "Admin", "Organizer", "User" };
            

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(this);


                if (!Roles.Any(r => r.Name == role))
                {
                    IdentityRole newRole = new IdentityRole(role);
                    newRole.NormalizedName = role.Normalize().ToUpperInvariant(); 
                    await roleStore.CreateAsync(newRole);
                }
            }


            //Add user to each role

            await userManager.AddToRoleAsync(adminUser, "Admin");
            await userManager.AddToRoleAsync(adminUser, "Organizer");
            await userManager.AddToRoleAsync(OrganizerUser, "Organizer");
            await userManager.AddToRoleAsync(User, "User");

            //Add events

            Events[] Events = new Events[] {
                new Events(){
                    Title = "Kungälv discgolf day",
                    Description = "We play discgolf for fun, everyone is welcome!",
                    Place = "Kungälv",
                    Address = "Stigvägen, 34",
                    Date = DateTime.Parse("4/04/2021 16:00"),
                    SpotsAvailable = 40,
                },
                new Events(){
                    Title="Moonhaven",
                    Description="Best lazertag in the world",
                    Place="Blackpark",
                    Address="510 N McPherson Church Rd Fayetteville, NC 28303",
                    Date=DateTime.Now.AddDays(12),
                    SpotsAvailable=23,
                },
                 new Events(){
                    Title = "Jokkmokk Frisbee tour",
                    Description = "Join our tournament and win prices from our sponsor Kastaplast",
                    Place = "Jokkmokk",
                    Address = "Älgstigen, 109",
                    Date = DateTime.Parse("5/06/2021 11:00:00"),
                    SpotsAvailable = 40,
                },
            };
                
            await AddRangeAsync(Events);

            await SaveChangesAsync();
        }

    }
}