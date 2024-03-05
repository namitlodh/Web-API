using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_layer.RequestModel
{
    public class RegisterModel
    {
        [RegularExpression("^[A-Z][a-z]{2,}$",ErrorMessage ="Input should starts with caps and should have minimum 3 letters")]
        public string FirstName { get; set; }
        [RegularExpression("^[A-Z][a-z]{2,9}$", ErrorMessage ="maximum length should be 10")]
        public string LastName { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+[@](gmail|yahoo|srmist)(.com|.edu|.in)$", ErrorMessage = "Provide a valid Gmail address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
