using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_layer.RequestModel;
using Microsoft.EntityFrameworkCore;
using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        public NoteEntity Pin(int NotesId) 
        {
            var pin = fundoContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
            if (pin != null)
            {
                if (pin.IsPin)
                {
                    pin.IsPin = false;
                    fundoContext.SaveChanges();
                }
                else
                {
                    pin.IsPin = true;
                }
                return pin;
            }
            else
            {
                throw new Exception("Data not found");
            }
        }
        public NoteEntity Colour(int NotesId)
        {
            var colour = fundoContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
            if(colour != null)
            {
                colour.Colour = "Blue";
                fundoContext.SaveChanges();
            }
            return colour;
        }
        public NoteEntity Reminder(int NotesId)
        {
            var reminder = fundoContext.Notes.FirstOrDefault(notes => notes.NotesId == NotesId);
            if(reminder != null)
            {
                reminder.Reminder = DateTime.UtcNow;
                fundoContext.SaveChanges();
            }
            return reminder;
        }
        public string UploadImage(string filepath, int NotesId, int Id)
        {
            try
            {
                var filterid = fundoContext.Notes.Where(user => user.Id == Id);
                if(filterid!=null)
                {
                    var findNotes = filterid.FirstOrDefault(notes => notes.NotesId == NotesId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dfsswxqdn", "913586421619367", "C958eogB65GJaEbl3PV0_WUvGRY");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filepath),
                            PublicId = findNotes.Title
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        fundoContext.SaveChanges();
                        return "Upload Successfull";
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Review
        public NoteEntity Getnotes(string title, string description)
        {
            var note = fundoContext.Notes.FirstOrDefault(note=>note.Title == title && note.Description == description);
            if(note != null)
            {
                return note;
            }
            else
            {
                return null;
            }
        }
        public int Count(int id)
        {
            var countnotes= fundoContext.Notes.FirstOrDefault(notes=>notes.Id == id);
            if(countnotes != null)
            {
                int count = fundoContext.Notes.Count(c=>c.Id==id);
                return count;
            }
            else
            {
                return 0;
            }
        }
    }
}
