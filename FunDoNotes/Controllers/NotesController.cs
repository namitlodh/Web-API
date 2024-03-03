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
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateNote(UpdateNoteModel model, int Notesid) 
        {
            var repsonse = notemanager.Update(model, Notesid);
            if (repsonse != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Update Note Success", Data = repsonse });

            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Update Note Failed", Data = repsonse });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult TrashNote(int NotesId)
        {
            var response = notemanager.Trash(NotesId);
            if (response != null)
            {

                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Trash Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Trash Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(int NotesId,int Id) 
        {
            try
            {
                int uid= Convert.ToInt32(User.FindFirst("Id").Value);
                var response = notemanager.Delete(NotesId,uid);
                if (response != null)
                {

                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Deleted Successfully", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Deletion Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult ArchiveNote(int NotesId)
        {
            try
            {
                var response = notemanager.Archive(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Archived Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Archive Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public ActionResult PinNote(int NotesId)
        {
            try
            {
                var response = notemanager.Pin(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Pinned Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Pin Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Colour")]
        public ActionResult ColourNote(int NotesId)
        {
            var response = notemanager.Colour(NotesId);
            if (response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Coloured Successfully", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Colour Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Reminder")]
        public ActionResult ReminderNote(int NotesId)
        {
            var response = notemanager.Reminder(NotesId);
            if (response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Reminder Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Reminder Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UploadImage")]
        public ActionResult UploadImageNote(string filepath, int NotesId, int Id)
        {
            var response = notemanager.UploadImage(filepath,NotesId,Id);
            if (response != null)
            {
                return Ok(new ResModel<string> { Success = true, Message = "Image Uploaded Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "Image Uploaded Failed", Data = response });
            }
        }
    }
}
