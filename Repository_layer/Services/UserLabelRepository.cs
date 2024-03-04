using Microsoft.EntityFrameworkCore;
using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_layer.Services
{
    public class UserLabelRepository:IUserLabelRepository
    {
        private readonly FundoContext fundoContext;
        public UserLabelRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public UserLabelEntity AddLabelNotes(int UserId, int NoteId, string LabelNames)
        {
            UserLabelEntity userLabelEntity = new UserLabelEntity();
            var addlabel=fundoContext.UserLabels.FirstOrDefault(label=>label.Id == UserId && label.NotesId==NoteId);
            if(addlabel == null) 
            {
                userLabelEntity.LabelName = LabelNames;
                userLabelEntity.NotesId = NoteId;
                userLabelEntity.Id = UserId;
                fundoContext.UserLabels.Add(userLabelEntity);
                fundoContext.SaveChanges();
                return userLabelEntity;
            }
            return userLabelEntity;
        }

    }
}
