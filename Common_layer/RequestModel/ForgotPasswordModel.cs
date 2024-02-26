using System;
using System.Collections.Generic;
using System.Text;

namespace Common_layer.RequestModel
{
    public class ForgotPasswordModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
