using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationManager collaborationManager;
        public CollaborationController(ICollaborationManager collaborationManager)
        {
            this.collaborationManager = collaborationManager;
        }
        [Authorize]
        [HttpPost]
        [Route("Addcolab")]
        public ActionResult Addcollba(int Noteid, string email)
        {
            int Id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = collaborationManager.AddCollab(Id, Noteid, email);
            if (response != null)
            {
                return Ok(new ResModel<CollaborationEntity> { Success = true, Message = "collaboration added", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<CollaborationEntity> { Success = false, Message = "collaboration not added", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Fetch")]
        public ActionResult Fetch(int Noteid)
        {
            int Id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = collaborationManager.FetchCollaboator(Id, Noteid);
            if (response != null)
            {
                return Ok(new ResModel<List<string>> { Success = true, Message = "collaboration added", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<List<string>> { Success = false, Message = "collaboration not added", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult TrashCollab(int NoteId,string email)
        {
            int Id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = collaborationManager.TrashCollab(Id, NoteId,email);
            if (response != null)
            {
                return Ok(new ResModel<CollaborationEntity> { Success = true, Message = "collaboration trashed", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<CollaborationEntity> { Success = false, Message = "collaboration not trashed", Data = response });
            }
        }
    }
}
