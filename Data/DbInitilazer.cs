using Core.Concrets;
using Core.Entities;
using U = Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbInitilazer
    {
        static readonly U.UnitOfWork _unitOfWork;

        static DbInitilazer()
        {
            _unitOfWork = new();
        }

        public static void SeedAdmin()
        {
            if (!_unitOfWork.Admin.GetAll().Any())
            {
                Admin admin = new() { Name = "admin", Surname = "admin", Email = "admin@gmail.com" };

                PasswordHasher<Admin> passwordHasher = new();
                admin.Password = passwordHasher.HashPassword(admin, "Admin-123");

                _unitOfWork.Admin.Add(admin);

                _unitOfWork.Commit();
            }
        }
    }
}
