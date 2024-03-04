using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interfaces
{
    public interface IUserLabelManager
    {
        public UserLabelEntity AddLabelNotes(int UserId, int NoteId, string LabelNames);
        public List<UserLabelEntity> GetLabel(int id, string labelNames);
        public UserLabelEntity Updatelabel(int id, int Noteid, string newname);
    }
}
