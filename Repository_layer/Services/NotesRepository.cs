using Common_layer.RequestModel;
using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_layer.Services
{
    public class NotesRepository:INotesRepository
    {
        private readonly FundoContext fundoContext;
        public NotesRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public NoteEntity AddNote(AddNoteModel model,int id)
        {
            NoteEntity note = new NoteEntity();
            note.Title = model.Title;
            note.Description = model.Description;
            note.Id = id;
            note.Reminder = DateTime.Now;
            note.Colour = null;
            note.Image = null;
            note.IsArchive = false;
            note.IsPin = false;
            note.IsTrash = false;
            note.CreatedAt = DateTime.Now;
            note.UpdatedAt = DateTime.Now;
            note.NotesUser = null;
            fundoContext.Notes.Add(note);
            fundoContext.SaveChanges();
            return note;
        }
        //public List<NoteEntity> GetAll(int id)
        //{
        //    return fundoContext.Notes.Where(x=> x.Id == id).ToList();
        //}
    }
}
