using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interfaces
{
    public interface ICollaborationManager
    {
        public CollaborationEntity AddCollab(int Id, int Noteid, string email);
        public List<string> FetchCollaboator(int Id, int NoteId);
    }
}
