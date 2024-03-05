﻿using Common_layer.RequestModel;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interfaces
{
    public interface IUserManager
    {
        public User UserRegisteration(RegisterModel model);
        public string UserLogin(LoginModel model);
        public ForgotPasswordModel ForgotPassword(string email);
        public string GenerateToken(string Email, int Id);
        public bool IsEmailAlreadyRegistered(string email);
        public bool ResetPassword(string Email, ResetPasswordModel model);
    }
}
