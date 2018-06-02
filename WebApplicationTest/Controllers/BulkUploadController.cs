using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplicationTest.Models;
using WebApplicationTest.BusinessLayer;
using WebApplicationTest.ViewModels;
using System.IO;

namespace WebApplicationTest.Controllers
{
    [AllowAnonymous]
    public class BulkUploadController : Controller
    {
        // GET: BulkUpload
        public ActionResult Index()
        {
            FileUploadViewModel fileUpload = new FileUploadViewModel();
            FooterViewModel FooterData = new FooterViewModel();
            FooterData.CompanyName = "DYD Creative Solution";//Can be set to dynamic value
            FooterData.Year = DateTime.Now.Year.ToString();
            fileUpload.FooterData = FooterData;
            return View(fileUpload);
        }

        public ActionResult Upload(FileUploadViewModel model)
        {
            List<Employee> employees = GetEmployees(model);
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
        }
        public ActionResult UploadEmployeeInfo(FileUploadViewModel model)
        {
            List<AreaModel> areaInfo = GetEmployeesInfo(model);
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployeesInfo(areaInfo);
            return RedirectToAction("Index", "Employee");
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine(); // Assuming first line is header
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');//Values are comma separated
                if (!values[0].Equals("Id"))
                {
                    Employee e = new Employee();
                    e.Id = values[0];
                    e.DateInclude = values[1];
                    e.email = values[2];
                    e.event1 = values[3];
                    e.url = values[9];
                    employees.Add(e);
                }
            }
            return employees;
        }

        private List<AreaModel> GetEmployeesInfo(FileUploadViewModel model)
        {
            List<AreaModel> employeesInfo = new List<AreaModel>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine(); // Assuming first line is header
            int i = 0;
            while (!csvreader.EndOfStream)
            {
                i++;
                var line = csvreader.ReadLine();
                var values = line.Split(',');//Values are comma separated
                if (values.Count() != 13)
                {
                    System.Diagnostics.Debug.WriteLine(values.Count());
                    System.Diagnostics.Debug.WriteLine(i);
                    foreach (var v in values)
                    {
                        System.Diagnostics.Debug.Write(v + "--");
                    }
                }
                
                if (!values[0].Equals("Employee ID"))
                {
                    AreaModel e = new AreaModel();
                    e.EmpId = values[0];
                    e.Email = values[1];
                    e.AreaCode = values[9];
                    e.Country = values[12];
                    employeesInfo.Add(e);
                }
            }
            return employeesInfo;
        }
    }
}