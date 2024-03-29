﻿using Common_layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class NoteManager:INotesManager
    {
        private readonly INotesRepository repository;
        public NoteManager(INotesRepository repository)
        {
            this.repository = repository;
        }
        public NoteEntity AddNote(AddNoteModel model, int Id)
        {
            return repository.AddNote(model,Id);
        }
        public List<NoteEntity> GetAll(int id)
        {
            return repository.GetAll(id);
        }
        public NoteEntity Update(UpdateNoteModel model, int NotesId)
        {
            return repository.Update(model,NotesId);    
        }
        public NoteEntity Trash(int NotesId)
        {
            return repository.Trash(NotesId);
        }
        public NoteEntity Delete(int NotesId, int Id)
        {
            return repository.Delete(NotesId, Id);
        }
        public NoteEntity Archive(int NotesId) 
        {  
            return repository.Archive(NotesId); 
        }
        public NoteEntity Pin(int NotesId)
        {
            return repository.Pin(NotesId);
        }
        public NoteEntity Colour(int NotesId)
        {
            return repository.Colour(NotesId);
        }
        public NoteEntity Reminder(int NotesId)
        {
            return repository.Reminder(NotesId);
        }
        public string UploadImage(string filepath, int NotesId, int Id)
        {
            return repository.UploadImage(filepath, NotesId, Id);
        }
        public NoteEntity Getnotes(string title, string description)
        {
            return repository.Getnotes(title, description);
        }
        public int Count(int id)
        {
            return repository.Count(id);
        }
    }
}
