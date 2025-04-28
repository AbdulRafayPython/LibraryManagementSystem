using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Library_Management_System.Models
{
    public class Member
    {
        [Key]
        public int MemID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [StringLength(13, ErrorMessage = "Mobile number cannot be longer than 13 characters.")]
        [Display(Name = "Mobile No.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [Display(Name = "Email")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Member Type is required.")]
        [Display(Name = "Member Type")]
        public int MemTypeID { get; set; }

        // Navigation property to MemberType
        public UserType MemberType { get; set; }
    }
}
