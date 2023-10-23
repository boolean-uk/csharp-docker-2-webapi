using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [ForeignKey("Author")]
        [Column("authorid")]
        public int AuthorId { get; set; }

        [ForeignKey("Publisher")]
        [Column("publisherid")]
        public int PublisherId { get; set; }


    }
}
