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
    public class NotesRepository : INotesRepository
    {
        private readonly FundoContext fundoContext;
        public NotesRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public NoteEntity AddNote(AddNoteModel model,int Id)
        {
            NoteEntity note = new NoteEntity();
            note.Id = Id;
            note.Title = model.Title;
            note.Description = model.Description;
            note.Reminder = model.Reminder;
            note.Colour = model.Colour;
            note.Image = model.Image;
            note.IsArchive = model.IsArchive;
            note.IsPin = model.IsPin;
            note.IsTrash = model.IsTrash;
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;
            fundoContext.Notes.Add(note);
            fundoContext.SaveChanges();
            return note;
        }
        public List<NoteEntity> GetAll(int id)
        {
            return fundoContext.Notes.Where(x => x.Id == id).ToList();
        }
    }
}
