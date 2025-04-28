using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Library_Management_System.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Book Title is required.")]
        [Display(Name = "Book Title")]
        [StringLength(200)]
        public string BookTitle { get; set; }

        [Display(Name = "Category ID")]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public BookCategory Category { get; set; }

        [Display(Name = "Publisher ID")]
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public BookPublisher Publisher { get; set; }

        [Display(Name = "Publication Year")]
        public int PublicationYear { get; set; }

        [Display(Name = "Book Edition")]
        [StringLength(50)]
        public string BookEdition { get; set; }

        [Display(Name = "Total Copies")]
        public int CopiesTotal { get; set; }

        [Display(Name = "Available Copies")]
        public int CopiesAvailable { get; set; }
    }
}