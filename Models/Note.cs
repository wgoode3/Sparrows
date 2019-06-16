using System;
using System.ComponentModel.DataAnnotations;

namespace Sparrows.Models
{
    public class Note
    {
        [Key]
        public int NoteId {get;set;}
        [Required (ErrorMessage = "Note Title is required!")]
        public string Title {get;set;}
        [Required (ErrorMessage = "Note Text is required!")]
        public string Text {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}