using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common_layer.RequestModel
{
    public class AddNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
