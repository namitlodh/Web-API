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
using System.Linq;
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
            entity.SetPassword(model.Password);
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
        public string UserLogin(LoginModel model)
        {
            try
            {
                var user = context.users.FirstOrDefault(a => a.Email == model.Email);
                if (user != null)
                {
                    if (user.VerifyPassword(model.Password))
                    {
                        var token = GenerateToken(user.Email, user.Id);
                        return token;
                    }
                    else
                    {
                        throw new Exception("Incorrect password");
                    }
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed {ex.Message}");
                return null;
            }
        }

        
        public string GenerateToken(string Email, int Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgotPassword(string email)
        {
            var user = context.users.FirstOrDefault(a=>a.Email == email);
            if (user != null)
            {
                var token = GenerateToken(user.Email, user.Id);
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
