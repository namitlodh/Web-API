using Manager_Layer.Interfaces;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class UserLabelManager:IUserLabelManager
    {
        private readonly IUserLabelRepository repository;
        public UserLabelManager(IUserLabelRepository repository)
        {
            this.repository = repository;
        }
        public UserLabelEntity AddLabelNotes(int UserId, int NoteId, string LabelNames)
        {
            return repository.AddLabelNotes(UserId, NoteId, LabelNames);
        }
    }
}
