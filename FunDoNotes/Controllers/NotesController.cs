using Common_layer.RequestModel;
using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_layer.Entity;
using System.Collections.Generic;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notemanager;
        private readonly IUserManager usermanager;
        public NotesController(INotesManager notemanager,IUserManager usermanager)
        {
            this.notemanager = notemanager;
            this.usermanager = usermanager;
        }
        [HttpPost]
        [Route("Add")]
        public ActionResult CreateNotes(AddNoteModel model)
        {
            var response = notemanager.AddNote(model);
            if (response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "note added", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "not added", Data = response });
            }
        }
    }
}
