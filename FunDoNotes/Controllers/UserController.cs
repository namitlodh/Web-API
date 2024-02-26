using Common_layer.RequestModel;
using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_layer.Entity;
using System;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Reg")]
        public  ActionResult Register(RegisterModel model)
        {
            try
            {
                var repsonse = userManager.UserRegisteration(model);
                if (repsonse != null)
                {
                    return Ok(new ResModel<User> { Success = true, Message = "register successfull", Data = repsonse });
                }
                else
                {
                    return BadRequest(new ResModel<User> { Success = false, Message = "Resgister failed", Data = repsonse });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<User> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [HttpPost]
        [Route("Log")]
        public ActionResult Login(LoginModel model)
        {
            string response = userManager.UserLogin(model);
            if (response != null)
            {
                return Ok(new ResModel<string> { Success=true, Message="Login successfull", Data=response});
            }
            else
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "Login failed", Data = response });
            }
        }
    }
}
