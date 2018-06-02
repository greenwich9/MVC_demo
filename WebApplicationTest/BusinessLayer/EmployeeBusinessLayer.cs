using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationTest.Models;

using WebApplicationTest.DataAccessLayer;

namespace WebApplicationTest.BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }

        public List<AreaModel> GetAreaInfo()
        {
            System.Diagnostics.Debug.WriteLine("here333");
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.AreaInfo.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL saleERP = new SalesERPDAL();
            saleERP.Employees.Add(e);
            saleERP.SaveChanges();
            return e;
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Sukesh" && u.Password == "Sukesh")
            {
                return UserStatus.AuthentucatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }

        public void UploadEmployeesInfo(List<AreaModel> employeesInfo)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.AreaInfo.AddRange(employeesInfo);
            salesDal.SaveChanges();
        }

        public void UploadEmployeesRegionInfo(List<AreaModel> areaInfo)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.AreaInfo.AddRange(areaInfo);
            salesDal.SaveChanges();
        }
    }
}