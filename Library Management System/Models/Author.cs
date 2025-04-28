using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class Author
    {
        [Key]
        [Display(Name ="Author ID")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage ="Author name is required!")]
        [StringLength(100)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }


    }
}