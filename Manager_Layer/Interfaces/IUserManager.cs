using Common_layer.RequestModel;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interfaces
{
    public interface IUserManager
    {
        public User UserRegisteration(RegisterModel model);
    }
}
