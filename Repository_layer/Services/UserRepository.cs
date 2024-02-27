using Common_layer.RequestModel;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using Repository_layer.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Repository_layer.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly FundoContext context;
        private readonly IConfiguration config;
        public UserRepository(FundoContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        public User UserRegisteration(RegisterModel model)
        {
            User entity = new User();
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Password = Encryption("eergewweterg4tq3rewgq34t343g3tky", model.Password);
            User user = context.users.FirstOrDefault(a => a.Email == model.Email);
            if (user != null)
            {
                throw new Exception("Email already exist");
            }
            else
            {
                context.users.Add(entity);
                context.SaveChanges();
                return entity;
            }
        }
    }
}
