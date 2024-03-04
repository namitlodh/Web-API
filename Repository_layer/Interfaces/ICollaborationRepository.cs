using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Interfaces
{
    public interface ICollaborationRepository
    {
        public CollaborationEntity AddCollab(int Id, int Noteid, string email);
    }
}
