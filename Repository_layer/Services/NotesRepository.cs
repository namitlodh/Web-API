using Common_layer.RequestModel;
using Microsoft.EntityFrameworkCore;
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
        public NoteEntity Update(UpdateNoteModel model,int NotesId)
        {
            var noteToUpdate = fundoContext.Notes.FirstOrDefault(notes=>notes.NotesId == NotesId);
            if (noteToUpdate != null)
            {
                noteToUpdate.Title= model.Title;
                noteToUpdate.Description= model.Description;
                noteToUpdate.UpdatedAt = DateTime.UtcNow;
                fundoContext.SaveChanges();
                return noteToUpdate;
            }
            return null;
        }
        public NoteEntity Trash(int NotesId)
        {
            var trash = fundoContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
            if(trash != null)
            {
                if (trash.IsTrash)
                {
                    trash.IsTrash = false;
                    fundoContext.SaveChanges();

                }
                else
                {
                    trash.IsTrash = true;
                }
            }
            return trash;
        }
        public NoteEntity Delete(int NotesId, int Id)
        {
            var delete = fundoContext.Notes.FirstOrDefault(notes=>(notes.NotesId==NotesId) && (notes.Id== Id));
            if(delete != null)
            {
                fundoContext.Notes.Remove(delete);
                fundoContext.SaveChanges();
            }
            else
            {
                throw new Exception("Wrong data");
            }
            return delete;
        }
        public NoteEntity Archive(int NotesId)
        {
            var archive = fundoContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
            if (archive != null)
            {
                if (archive.IsArchive)
                {
                    archive.IsArchive = false;
                    fundoContext.SaveChanges();
                }
                else
                {
                    archive.IsArchive = true;
                }
                return archive;
            }
            else
            {
                throw new Exception("Data not found");
            }
        }
    }
}
