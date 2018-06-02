using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplicationTest.ViewModels;
using WebApplicationTest.BusinessLayer;
using WebApplicationTest.Models;
using WebApplicationTest.Filter;

namespace WebApplicationTest.Controllers
{
    [AllowAnonymous]
    public class EmployeeController : Controller
    {
        // GET: Test
        [Authorize]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            employeeListViewModel.UserName = User.Identity.Name;
            List<AreaModel> areaInfo = new List<AreaModel>();
            areaInfo = empBal.GetAreaInfo();

            var eply = from m in employees
                       select m;

            var email = from m in employees
                        select m.email;
            employeeListViewModel.uniqueUser = email.Distinct().Count();

            //var dateInfo = from m in employees
            //               join region in areaInfo on m.email equals region.Email
            //               where m.event1 == "open"
            //               orderby DateTime.Parse(m.DateInclude).Year.ToString() + "-" + DateTime.Parse(m.DateInclude).Month.ToString() + "-" + DateTime.Parse(m.DateInclude).Day.ToString()
            //               group m by new { date = DateTime.Parse(m.DateInclude).Year.ToString() + "-" + DateTime.Parse(m.DateInclude).Month.ToString() + "-" + DateTime.Parse(m.DateInclude).Day.ToString(), code = region.AreaCode } into grp
            //               select new { key = grp.Key, cnt = grp.Count() };

            var dateInfo = from m in employees
                           where m.event1 == "open"
                           orderby DateTime.Parse(m.DateInclude).Year.ToString() + "-" + DateTime.Parse(m.DateInclude).Month.ToString() + "-" + DateTime.Parse(m.DateInclude).Day.ToString()
                           group m by DateTime.Parse(m.DateInclude).Year.ToString() + "-" + DateTime.Parse(m.DateInclude).Month.ToString() + "-" + DateTime.Parse(m.DateInclude).Day.ToString() into grp
                           select new { key = grp.Key, cnt = grp.Count() };
            List<DateCount> dateCount = new List<DateCount>();
            foreach (var item in dateInfo) {
                DateCount date = new DateCount();
                date.date = item.key;
               
                date.count = item.cnt;
                dateCount.Add(date);
            }
            employeeListViewModel.dateCount = dateCount;



            var clickUser = from m in employees
                            where m.event1 == "click"
                            select m.email;
            int uniqueClickUser = clickUser.Distinct().Count();
            employeeListViewModel.uniqueClickUser = uniqueClickUser;

            var urlInfo = from m in employees
                          where m.url != ""
                          group m by m.url.Split('/')[m.url.Split('/').Count() - 1] into grp
                          orderby grp.Count() descending
                          select new { key = grp.Key, cnt = grp.Count() };
            List<UrlCount> urlCount = new List<UrlCount>();
            foreach (var item in urlInfo)
            {
                UrlCount url = new UrlCount();
                url.url = item.key;
                url.count = item.cnt;
                urlCount.Add(url);
            }
            employeeListViewModel.urlCount = urlCount;

            var open = eply.Where(s => s.event1.Equals("open"));
            var click = eply.Where(s => s.event1.Equals("click"));
            var processed = eply.Where(s => s.event1.Equals("processed"));
            var delivered = eply.Where(s => s.event1.Equals("delivered"));
            var deferred = eply.Where(s => s.event1.Equals("deferred"));
            employeeListViewModel.openCount = open.Count();
            employeeListViewModel.clickCount = click.Count();
            employeeListViewModel.processedCount = processed.Count();
            employeeListViewModel.deliveredCount = delivered.Count();
            employeeListViewModel.deferredCount = deferred.Count();

            

            //System.Diagnostics.Debug.WriteLine("here");
            //foreach (var a in areaInfo)
            //{
            //    System.Diagnostics.Debug.WriteLine(a.EmpId);
            //}
            //System.Diagnostics.Debug.WriteLine("here2");

            var regionCode = from m in employees
                             where m.event1 == "open"
                             join code in areaInfo on m.email equals code.Email
                             group code by code.AreaCode into grp 
                             select new { code = grp.Key, cnt = grp.Distinct().Count() };
            List<regionCodeCount> regionCodeCount = new List<regionCodeCount>();
            foreach (var item in regionCode)
            {
                regionCodeCount region = new regionCodeCount();
                region.regionCode = item.code;
                region.count = item.cnt;
                regionCodeCount.Add(region);
            }
            employeeListViewModel.regionCodeCount = regionCodeCount;


            var countrySet = from m in employees
                             where m.event1 == "open"
                             join code in areaInfo on m.email equals code.Email
                             group code by code.Country into grp
                             select new { code = grp.Key, cnt = grp.Distinct().Count(), regionCode = grp.First().AreaCode };
            List<countryCount> america = new List<countryCount>();
            List<countryCount> asianPacific = new List<countryCount>();
            List<countryCount> europe = new List<countryCount>();
            foreach (var country in countrySet)
            {
                countryCount cty = new countryCount();
                cty.country = country.code;
                cty.count = country.cnt;
                switch (country.regionCode)
                {
                    case "AMS":
                        america.Add(cty);
                        break;
                    case "EUR":
                        europe.Add(cty);
                        break;
                    case "APJ":
                        asianPacific.Add(cty);
                        break;
                }
            }
            employeeListViewModel.america = america;
            employeeListViewModel.europe = europe;
            employeeListViewModel.asianPacific = asianPacific;

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.Id = emp.Id;
                empViewModel.email = emp.email;
                empViewModel.event1 = emp.event1;
                empViewModel.timestamp = emp.timestamp;
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "DYD Creative Solution";//Can be set to dynamic value
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", employeeListViewModel);

        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            employeeListViewModel.UserName = User.Identity.Name; //New Line
            return View("CreateEmployee", employeeListViewModel);
        }



        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult Search()
        {
            string val = Request.Form["Event"];
            EmployeeListViewSearchModel employeeListSearchViewModel =
                     new EmployeeListViewSearchModel();

            EmployeeBusinessLayer empBal =
                     new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels =
                     new List<EmployeeViewModel>();
            employeeListSearchViewModel.UserName = User.Identity.Name;
            employeeListSearchViewModel.totalCount = employees.Count();
            var eply = from m in employees
                            select m;

            System.Diagnostics.Debug.WriteLine("here");

            System.Diagnostics.Debug.WriteLine("test");
            System.Diagnostics.Debug.WriteLine(val);
            System.Diagnostics.Debug.WriteLine("value above");
            var em = eply.Where(s => s.event1.Equals(val));
            employeeListSearchViewModel.curCount = em.Count();
            foreach (Employee emp in em)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.Id = emp.Id;
                empViewModel.email = emp.email;
                empViewModel.event1 = emp.event1;
                empViewModel.timestamp = emp.timestamp;
                empViewModels.Add(empViewModel);
            }
            employeeListSearchViewModel.event1 = val;
            employeeListSearchViewModel.Employees = empViewModels;
            employeeListSearchViewModel.FooterData = new FooterViewModel();
            employeeListSearchViewModel.FooterData.CompanyName = "DYD Creative Solution";//Can be set to dynamic value
            employeeListSearchViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Search", employeeListSearchViewModel);
        }
    }
}