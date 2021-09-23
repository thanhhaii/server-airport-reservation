using eProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.Data.Extension
{
    public static class ModelBuilderExtention
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>().HasData(
                new Airline
                {
                    AirlineId = 1,
                    AirlineName = "Vietnam Airlines",
                    Vat = 10,
                    ScreeningSecurityFee = 2,
                    AirportFee = 9,
                    AdministrationFee = 35,
                    Surcharge = 3.5,
                    PriceNetFristClass = 3.5,
                    PriceNetBusinessClass = 2.5,
                    PriceNetPremiumEconomicClass = 1.5
                },
                new Airline
                {
                    AirlineId = 2,
                    AirlineName = "Vietjet Air",
                    Vat = 10,
                    ScreeningSecurityFee = 2,
                    AirportFee = 9,
                    AdministrationFee = 28,
                    Surcharge = 5.5,
                    PriceNetFristClass = 3,
                    PriceNetBusinessClass = 2,
                    PriceNetPremiumEconomicClass = 1.5
                },
                new Airline
                {
                    AirlineId = 3,
                    AirlineName = "Jetstar Pacific",
                    Vat = 10,
                    ScreeningSecurityFee = 1,
                    AirportFee = 7,
                    AdministrationFee = 10,
                    Surcharge = 5.5,
                    PriceNetFristClass = 3,
                    PriceNetBusinessClass = 2,
                    PriceNetPremiumEconomicClass = 1.5
                },
                new Airline
                {
                    AirlineId = 4,
                    AirlineName = "Bamboo Airways",
                    Vat = 1,
                    AdministrationFee = 32,
                    ScreeningSecurityFee = 15,
                    AirportFee = 8,
                    Surcharge = 5,
                    PriceNetFristClass = 3.2,
                    PriceNetBusinessClass = 2.3,
                    PriceNetPremiumEconomicClass = 1.5
                }
            );

            var roleAdminId = new Guid("726AB76E-B647-4C05-8365-79182FE8DD30");
            var roleUserId = new Guid("31F17161-B6E9-4EB3-978D-38BD7026278F");
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = roleAdminId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Role administrator"
                },
                new AppRole
                {
                    Id = roleUserId,
                    Name = "user",
                    NormalizedName = "user",
                    Description = "Role user"
                }
            );

            var hasher = new PasswordHasher<AppUser>();
            var adminId = new Guid("682E9E30-1E41-4236-A00E-5BF00ADD497B");
            var userId = new Guid("39A08ED5-B365-4138-A49C-F6350753E316");
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "eprojectSemester3@gmail.com",
                    NormalizedEmail = "eprojectSemester3@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Admin",
                    LastName = "EProject",
                    Dob = new DateTime(2020, 01, 31)
                },
                new AppUser
                {
                    Id = userId,
                    UserName = "user",
                    NormalizedUserName = "user",
                    Email = "user@gmail.com",
                    NormalizedEmail = "user@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "User",
                    LastName = "EProject",
                    Dob = new DateTime(2020, 01, 31)
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = roleAdminId,
                    UserId = adminId
                }, 
                new IdentityUserRole<Guid>
                {
                    RoleId = roleUserId,
                    UserId = userId
                }
            );
        }

    }
}
