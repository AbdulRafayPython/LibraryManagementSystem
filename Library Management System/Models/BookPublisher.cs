using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class BookPublisher
    {
        [Key]
        public int PublisherID { get; set; }

        [Required(ErrorMessage = "Publisher name is required.")]
        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }

        [Required(ErrorMessage = "Publication language is required.")]
        [Display(Name = "Publication Language")]
        public string PublicationLanguage { get; set; }

        [Required(ErrorMessage = "Publication type is required.")]
        [Display(Name = "Publication Type")]
        public string PublicationType { get; set; }
    }
}