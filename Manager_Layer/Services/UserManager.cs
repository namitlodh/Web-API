using Common_layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository) 
        {
            this.repository = repository;
        }
        public User UserRegisteration(RegisterModel model)
        {
            return repository.UserRegisteration(model);
        }
    }
}
