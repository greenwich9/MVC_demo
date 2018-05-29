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
        public string Id { get; set; }


        public string event1 { get; set; }

 
        public string timestamp { get; set; }
      
        public string email { get; set; }
      
    }
}