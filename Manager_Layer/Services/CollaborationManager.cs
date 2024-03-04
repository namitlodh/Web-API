using Manager_Layer.Interfaces;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class CollaborationManager:ICollaborationManager
    {
        private readonly ICollaborationRepository repository;
        public CollaborationManager(ICollaborationRepository repository)
        {
            this.repository = repository;
        }
        public CollaborationEntity AddCollab(int Id, int Noteid, string email)
        {
            return repository.AddCollab(Id, Noteid, email);
        }
        public List<string> FetchCollaboator(int Id, int NoteId)
        {
            return repository.FetchCollaboator(Id, NoteId);
        }
        public CollaborationEntity TrashCollab(int Id, int NoteId, string email)
        {
            return repository.TrashCollab(Id, NoteId, email);
        }
    }
}
