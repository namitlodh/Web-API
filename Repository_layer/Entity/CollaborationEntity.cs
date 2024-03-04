using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_layer.Entity
{
    public class CollaborationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollabId { get; set; }
        public string CollanEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public bool IsTrash {  get; set; }
        [ForeignKey("NotesUser")]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual User NotesUser { get; set; }
        [ForeignKey("NoteEntity")]
        public int NotesId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity NoteEntity { get; set; }
    }
}
