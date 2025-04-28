using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class UserType
    {
        [Key]
        public int UserTypeID {  get; set; }
        public string UserTypeName { get; set; }
    }
}