using Common_layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class UserManager : IUserManager
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
        public string UserLogin(LoginModel model)
        {
            return repository.UserLogin(model);
        }
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return repository.ForgotPassword(email);
        }
        public string GenerateToken(string Email, int Id)
        {
            return repository.GenerateToken(Email, Id);
        }
        public bool IsEmailAlreadyRegistered(string email)
        {
            return repository.IsEmailAlreadyRegistered(email);
        }
        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            return repository.ResetPassword(Email, model);
        }
        public User CheckUser(RegisterModel model, int id)
        {
            return repository.CheckUser(model, id);
        }
    }
}
