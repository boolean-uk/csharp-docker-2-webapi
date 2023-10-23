using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models
{
    [Table("authors")]
    public class Author
    {
        [Column("id")]
        public int Id {get;set;}
    
        [Column("firstname")]
        public string Firstname {get;set;}
        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("email")]
        public string Email{get;set;}

    }
}