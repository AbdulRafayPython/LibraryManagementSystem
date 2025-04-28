using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class BookAuthor
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Book ID is required.")]
        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Author ID is required.")]
        [Display(Name = "Author ID")]
        public int AuthorID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }

        [ForeignKey("AuthorID")]
        public virtual Author Author { get; set; }
    }
}