using Common_layer.RequestModel;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Interfaces
{
    public interface IUserRepository
    {
        public User UserRegisteration(RegisterModel model);
        public string UserLogin(LoginModel model);
        public string ForgotPassword(string email);
        public string GenerateToken(string Email, int Id);
    }
}
