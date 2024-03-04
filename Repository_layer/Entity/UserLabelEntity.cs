using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_layer.Entity
{
    public class UserLabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
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
