using Event.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Context
{
    public class DatabaseContext : IdentityDbContext<MyUser>
    {


      // public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Events> Event { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public void Seed()
        {

            Database.EnsureCreated();


            //List<Organizer> OrganizersList = new List<Organizer>()
            //{
            //    new Organizer
            //    {
            //        Name = "Johan Eriksson",
            //        Email = "johan.frisbee@gmail.com",
            //        PhoneNumber = "0722923781"
            //    },
            //    new Organizer
            //    {
            //        Name = "Erik Malmberg",
            //        Email = "malmbergsfrisbee@gmail.com",
            //        PhoneNumber = "0722851374"
            //    },
            //    new Organizer
            //    {
            //        Name = "Nils Karlsson",
            //        Email = "karlsson@gmail.com",
            //        PhoneNumber = "0722193528"
            //    }
            //};
            List<Events> EventsList = new List<Events>()
            {
                new Events
                {
                    Title = "Ale open",
                //    Organizer = OrganizersList[0],
                    Description = "Come play on one of Swedens greatest discgolf courses",
                    Place = "Stengunsund",
                    Address = "Hasselbacken, 13",
                    Date = DateTime.Parse("4/22/2021 18:00"),
                    SpotsAvailable = 30,


                },
                new Events
                {
                    Title = "Kungälv discgolf day",
               //     Organizer = OrganizersList[1],
                    Description = "We play discgolf for fun, everyone is welcome!",
                    Place = "Kungälv",
                    Address = "Stigvägen, 34",
                    Date = DateTime.Parse("4/04/2021 16:00"),
                    SpotsAvailable = 40,

                },
                new Events
                {
                    Title = "Jokkmokk Frisbee tour",
                   // Organizer = OrganizersList[2],
                    Description = "Join our tournament and win prices from our sponsor Kastaplast",
                    Place = "Jokkmokk",
                    Address = "Älgstigen, 109",
                    Date = DateTime.Parse("5/06/2021 11:00:00"),
                    SpotsAvailable = 40,

                }
            };
          //  Organizers.AddRange(OrganizersList);
            Event.AddRange(EventsList);

            SaveChanges();

        }
    }
}
