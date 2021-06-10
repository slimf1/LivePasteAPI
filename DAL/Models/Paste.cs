using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Paste
    {
        public int PasteID { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string Language { get; set; }
    }
}
