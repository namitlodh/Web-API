using Common_layer.RequestModel;
using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using Repository_layer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly FundoContext context;
        public UserRepository(FundoContext context)
        {
            this.context = context;
        }
        public User UserRegisteration(RegisterModel model)
        {
            User entity = new User();
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Password = model.Password;
            context.users.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
