using Common_layer;
using Common_layer.RequestModel;
using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Repository_layer.Context;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
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
        private readonly ILogger<UserController> logger;
        private readonly IDistributedCache _cache;
        public UserController(IUserManager userManager, IBus bus, FundoContext context1, ILogger<UserController> logger, IDistributedCache _cache)
        {
            this.userManager = userManager;
            this.bus = bus;
            this.context1= context1;
            this.logger = logger;
            this._cache = _cache;
        }

        [HttpPost]
        [Route("Reg")]
        public  ActionResult Register(RegisterModel model)
        {
            var repsonse = userManager.UserRegisteration(model);
            if(repsonse != null)
            {
                logger.LogInformation("Register Succesfull");
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
            try
            {
                string response = userManager.UserLogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<string> { Success = true, Message = "Login successful", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = "Login Failed", Data = response });
                }
            }

            catch (Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });

            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            try
            {
                if (userManager.IsEmailAlreadyRegistered(Email))
                {
                    Send send = new Send();
                    ForgotPasswordModel model = userManager.ForgotPassword(Email);
                    var checkmail = context1.users.FirstOrDefault(x => x.Email == Email);
                    send.SendMail(Email, model.Token);
                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(model);
                    return Ok(new ResModel<string> { Success = true, Message = "mail sent", Data = model.Token });
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = "mail not sent", Data = Email });
                }
            }
            catch 
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "mail not sent", Data = Email });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;
                if (userManager.ResetPassword(Email, model))
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Password changed", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Password not changed", Data = false });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetAll/{Id}")]
        public async Task<List<User>> GetAll(int Id)
        {
            string cacheKey = Id.ToString();

            // Trying to get data from the Redis cache
            byte[] cachedData = await _cache.GetAsync(cacheKey);
            List<User> userdetails = new List<User>();
            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                userdetails = JsonSerializer.Deserialize<List<User>>(cachedDataString);
            }
            else
            {
                // If the data is not found in the cache, then fetch data from database
                userdetails = context1.users.Where(x=>x.Id==Id).OrderByDescending(x => x.FirstName).ToList();

                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(userdetails);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);
            }
            return userdetails;
        }
    }
}

