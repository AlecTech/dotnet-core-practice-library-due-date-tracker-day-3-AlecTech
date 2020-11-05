using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebMVCBookApp.Models
{
    [Table("author")]
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column("Name", TypeName = "varchar(60)")]
        public string Name { get; set; }

        [Required]
        [Column("BirthDate", TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Column("DeathDate", TypeName = "date")]
        public DateTime DeathDate { get; set; }

        [InverseProperty(nameof(Models.Book.Author))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
