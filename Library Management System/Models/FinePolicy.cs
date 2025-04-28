using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Library_Management_System.Models
{
    public class FinePolicy
    {
        [Key]
        public int FinePolicyID { get; set; }

        [Required(ErrorMessage = "Maximum allowed borrow days is required.")]
        [Display(Name = "Maximum Borrow Days")]
        public int MaxDays { get; set; }

        [Required(ErrorMessage = "Fine amount is required.")]
        [Display(Name = "Fine Amount")]
        [DataType(DataType.Currency)]
        public decimal FineAmount { get; set; }

        [Required(ErrorMessage = "Member type is required.")]
        [Display(Name = "Member Type ID")] 
        public int MemTypeID { get; set; }

        [ForeignKey("MemTypeID")]
        public virtual UserType MemberType { get; set; }
    }
}