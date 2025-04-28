using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class BookReturn
    {
        [Key]
        public int ReturnID { get; set; } // Primary key

        [Required(ErrorMessage = "Book Issue ID is required for a book return.")]
        [DisplayName("Book Issue ID")]
        public int IssueID { get; set; } // Foreign key referencing BookIssue
        [ForeignKey("IssueID")]
        public virtual BookIssue BookIssue{ get; set; }

        [Required(ErrorMessage = "Return Date is required.")]
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

        [DisplayName("Fine Amount")]
        public decimal FineAmount { get; set; } 
    }
}