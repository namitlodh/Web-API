using Common_layer.RequestModel;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Interfaces
{
    public interface INotesRepository
    {
        public NoteEntity AddNote(AddNoteModel model,int Id);
        public List<NoteEntity> GetAll(int id);
        public NoteEntity Update(UpdateNoteModel model, int NotesId);
        public NoteEntity Trash(int NotesId);
        public NoteEntity Delete(int NotesId, int Id);
    }
}
