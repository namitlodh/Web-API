using Common_layer;
using Common_layer.RequestModel;
using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repository_layer.Context;
using Repository_layer.Entity;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly FundoContext context1;
        private readonly IBus bus;
        public UserController(IUserManager userManager, IBus bus, FundoContext context1)
        {
            this.userManager = userManager;
            this.bus = bus;
            this.context1= context1;
        }

        [HttpPost]
        [Route("Reg")]
        public  ActionResult Register(RegisterModel model)
        {
            var repsonse = userManager.UserRegisteration(model);
            if(repsonse != null)
            {
                return Ok(new ResModel<User> { Success = true, Message = "register successfull", Data = repsonse });
            }
            else
            {
                return BadRequest(new ResModel<User> { Success = false, Message = "Resgister failed", Data= repsonse });
            }
        }
        [HttpPost]
        [Route("Log")]
        public ActionResult Login(LoginModel model)
        {
            string response = userManager.UserLogin(model);
            if (response != null)
            {
                return Ok(new ResModel<string> { Success = true, Message = "Login successfull", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "Login failed", Data = response });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            Send send = new Send();
            var check = userManager.ForgotPassword(Email);
            var checkmail = context1.users.FirstOrDefault(x => x.Email == Email);
            var token = userManager.GenerateToken(checkmail.Email, checkmail.Id);
            send.SendMail(Email, token);
            Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(check);
            return Ok(new ResModel<string> { Success = true, Message = "mail sent", Data = token });

        }
    }
}
