﻿using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Interfaces
{
    public interface IUserLabelRepository
    {
        public UserLabelEntity AddLabelNotes(int UserId, int NoteId, string LabelNames);
    }
}
