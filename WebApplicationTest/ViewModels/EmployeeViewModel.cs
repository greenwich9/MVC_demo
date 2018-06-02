using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string email { get; set; }
        public string timestamp { get; set; }
        public string event1 { get; set; }
        public DateTime DateInclude { get; set; }
        public string url { get; set; }
    }
}