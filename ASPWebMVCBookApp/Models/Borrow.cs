using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWebMVCBookApp.Models
{
    [Table("borrow")]
    public class Borrow
    {
        //public Borrow()
        //{
        //    Books = new HashSet<Book>();
        //}

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column("CheckedOutDate", TypeName = "date")]
        public DateTime CheckedOutDate { get; set; }

        [Required]
        [Column("DueDate", TypeName = "date")]
        public DateTime DueDate { get; set; }

        [Column("ReturnedDate", TypeName = "date")]
        public DateTime? ReturnedDate { get; set; }

        [Required]
        [Column("BookID", TypeName = "int(10)")]
        public int BookID { get; set; }

        [Required]
        [Column("ExtensionCount", TypeName ="int(10)")]
        public int ExtensionCount { get; set; }

        [ForeignKey(nameof(BookID))]
        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Book.Borrows))]
        public virtual Book Book { get; set; }

    }
}
