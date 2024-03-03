using Common_layer.RequestModel;
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
    }
}
