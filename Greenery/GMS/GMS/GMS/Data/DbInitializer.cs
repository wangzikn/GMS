using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Models.BindingModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data
{
    /*
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly IIndividualResponsitory _userManager;

        public DbInitializer(ApplicationDbContext db, IIndividualResponsitory userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
            if (_db.Users.FirstOrDefault(x => x.UserName == "Admin") == null)
            {
                //await _userManager.AddAsync(new UserBindingModel
                //{
                //    UserName = "Admin",
                //    Email = "admin@gmail.com",
                //    PhoneNumber = "0123456789",
                //    FullName = "Admin",
                //    Password = "Admin@123*"
                //});
                string passWord = "Admin@123*";

               

            }
            

            
        }
    }*/

    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly IIndividualResponsitory _userManager;

        public DbInitializer(ApplicationDbContext db, IIndividualResponsitory userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (_db.Users.FirstOrDefault(x => x.UserName == "Admin") == null)
            {
                var result = await _userManager.AddAsync(new UserBindingModel
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0123456789",
                    FullName = "Admin",
                    Password = "Admin@123*"
                });
            }



        }
    }
}
