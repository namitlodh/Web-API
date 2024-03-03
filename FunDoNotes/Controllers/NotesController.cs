using Common_layer.RequestModel;
using Common_layer.ResponseModel;
using Manager_Layer.Interfaces;
using Manager_Layer.Services;
using MassTransit;
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
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notemanager;
        public NotesController(INotesManager notemanager)
        {
            this.notemanager = notemanager;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult CreateNotes(AddNoteModel model)
        {
            int Id = Convert.ToInt32(User.FindFirst("Id").Value);
            var response = notemanager.AddNote(model, Id);
            if (response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "note added", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "not added", Data = response });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetNote")]
        public ActionResult FetchData(int id)
        {
            List<NoteEntity> data = notemanager.GetAll(id);
            if (data != null)
            {

                return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Get Note Successful", Data = data });

            }
            else
            {
                return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "Get Note Failure", Data = null });
            }
        }
    }
}
