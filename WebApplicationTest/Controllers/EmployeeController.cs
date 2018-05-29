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
    public class EmployeeController : Controller
    {
        // GET: Test
        [Authorize]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel =
                     new EmployeeListViewModel();

            EmployeeBusinessLayer empBal =
                     new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels =
                     new List<EmployeeViewModel>();
            employeeListViewModel.UserName = User.Identity.Name;
            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel =
                      new EmployeeViewModel();
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
            foreach (var item in eply)
            {
                System.Diagnostics.Debug.WriteLine(item.event1.Equals(val));
            }

            System.Diagnostics.Debug.WriteLine("test");
            System.Diagnostics.Debug.WriteLine(val);
            System.Diagnostics.Debug.WriteLine("value above");
            var em = eply.Where(s => s.event1.Equals(val));
            employeeListSearchViewModel.curCount = em.Count();
            foreach (Employee emp in em)
            {
                EmployeeViewModel empViewModel =
                      new EmployeeViewModel();
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