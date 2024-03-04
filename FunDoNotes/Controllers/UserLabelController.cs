using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using Manager_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_layer.Entity;
using System;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLabelController : ControllerBase
    {
        private readonly IUserLabelManager userLabelManager;
        public UserLabelController(IUserLabelManager userLabelManager)
        {
            this.userLabelManager = userLabelManager;
        }
        [Authorize]
        [HttpPost]
        [Route("Addlabel")]
        public ActionResult Addlabel(int NoteId, string LabelNames)
        {
            int Id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = userLabelManager.AddLabelNotes(Id,NoteId,LabelNames);
            if (response != null)
            {
                return Ok(new ResModel<UserLabelEntity> { Success = true, Message = "label added", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<UserLabelEntity> { Success = false, Message = "label not added", Data = response });
            }
        }
    }
}
