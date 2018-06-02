using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationTest.Models;

namespace WebApplicationTest.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        //public IEnumerable<EmployeeListViewModel> processed { get; set; }
        //public IEnumerable<EmployeeListViewModel> delivered { get; set; }
        //public IEnumerable<EmployeeListViewModel> click { get; set; }
        //public IEnumerable<EmployeeListViewModel> deferred { get; set; }
        //public IEnumerable<EmployeeListViewModel> open { get; set; }
        public int processedCount { get; set; }
        public int deliveredCount { get; set; }
        public int clickCount { get; set; }
        public int deferredCount { get; set; }
        public int openCount { get; set; }
        public int uniqueUser { get; set; }
        public List<DateCount> dateCount { get; set; }
        public List<UrlCount> urlCount { get; set; }
        public int uniqueClickUser { get; set; }
        // public Dictionary<string, int> dic { get; set; }
        public List<regionCodeCount> regionCodeCount { get; set; }
        public List<countryCount> america { get; set; }
        public List<countryCount> asianPacific { get; set; }
        public List<countryCount> europe { get; set; }
    }
}