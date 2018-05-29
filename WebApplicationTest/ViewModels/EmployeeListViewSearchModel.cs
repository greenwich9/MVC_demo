using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.ViewModels
{
    public class EmployeeListViewSearchModel : EmployeeListViewModel
    {
        public int curCount { get; set; }
        public int totalCount { get; set; }
        public string event1 { get; set; }
    }
}