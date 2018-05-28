using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage ="can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "can't be empty")]
        public string LastName { get; set; }
      
    public int? Salary { get; set; }
      
    }
}