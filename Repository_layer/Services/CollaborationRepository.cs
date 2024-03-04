using Repository_layer.Context;
using Repository_layer.Entity;
using Repository_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_layer.Services
{
    public class CollaborationRepository:ICollaborationRepository
    {
        private readonly FundoContext fundoContext;
        public CollaborationRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public CollaborationEntity AddCollab(int Id,int Noteid, string email)
        {
            var checkmail=fundoContext.users.FirstOrDefault(x=>x.Email == email);
            if(checkmail!=null)
            {
                CollaborationEntity collaborationEntity = new CollaborationEntity();
                var collab=fundoContext.Notes.FirstOrDefault(x=>x.Id == Id && x.NotesId==Noteid);
                if(collab!=null)
                {
                    collaborationEntity.CollanEmail = email;
                    collaborationEntity.Id = Id;
                    collaborationEntity.NotesId = Noteid;
                    fundoContext.Collaborations.Add(collaborationEntity);
                    fundoContext.SaveChanges();
                    return collaborationEntity;
                }
                return null;
            }
            return null;
        }
        public List<string> FetchCollaboator(int Id,int NoteId)
        {
            List<string> ret = new List<string>();
            List<CollaborationEntity> collaborations = new List<CollaborationEntity>();
            collaborations = fundoContext.Collaborations.Where(x=>x.Id==Id && x.NotesId==NoteId).ToList();
            foreach (var collaboration in collaborations)
            {
                ret.Add(collaboration.CollanEmail);
            }
            return ret;
        }
    }
}
