using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class Librarian
    {
        [Display(Name = "ID")]
        public int IssuedByID { get; set; }

        [Display(Name = "Librarian Name")]
        public string StaffName { get; set; }
    }
}