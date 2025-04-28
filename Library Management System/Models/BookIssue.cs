using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class BookIssue
    {
        [Key]
        public int IssueID { get; set; }

        [Required(ErrorMessage ="Select Book!")]
        [Display(Name = "Book")]
        public int BookID { get; set; }

        public virtual Book Book { get; set; }

        [Required(ErrorMessage = "Select Member!")]
        [Display(Name = "Member")]
        public int MemID { get; set; }

        public Member Member { get; set; }
        public string FullMemberName { get; set; }

        [Required(ErrorMessage ="Please Select Issue Date!")]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)] 
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please Select Due Date!")]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)] 
        public DateTime DueDate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.Date)] 
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Issued By")]
        public int IssuedByID { get; set; }

        public virtual Login IssuedBy { get; set; } 
    }
}