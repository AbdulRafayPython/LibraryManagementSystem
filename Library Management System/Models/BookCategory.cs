using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class BookCategory
    {
        [Key]
        [Display(Name ="Category ID")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Book Category is required.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}