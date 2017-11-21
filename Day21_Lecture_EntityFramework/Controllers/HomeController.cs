using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Day21_Lecture_EntityFramework.Models;

namespace Day21_Lecture_EntityFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListCustomers ()
        {
            /***** the old way of doing sql queries in .NET/ASP *****/
            //SqlConnection c = new SqlConnection(@"Data Source=.\\sqlexpress01; Initial Catalog=northwind; "); //connection string

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = c;
            //cmd.CommandText = "SELECT * FROM CUSTOMERS";

            //c.Open();
            //cmd.ExecuteReader();


            /********************/
            //1. Create an object for the ORM
            NORTHWNDEntities ORM = new NORTHWNDEntities();

            //2. Load data from the DbSet into most of the available data structure in C#
            List<Customer> CustomerList = ORM.Customers.ToList();

            ViewBag.CustomerList = CustomerList;

            return View("CustomersView");
        }

        public ActionResult ListCustomersByCountry (string country)
        {
            //1. Create an object for the ORM
            NORTHWNDEntities ORM = new NORTHWNDEntities();

            //2. Load data from the DbSet into most of the available data structure in C#
            List<Customer> OutputList = new List<Customer>();
            foreach (Customer CustomerRecord in ORM.Customers.ToList())
            {
                if (CustomerRecord.Country.ToLower() == country.ToLower())
                {
                    OutputList.Add(CustomerRecord);
                }
            }

            ViewBag.CustomerList = OutputList;

            return View("CustomersView");
        }

        public ActionResult ListCustomersByCustName(string custName)
        {
            //1. Create an object for the ORM
            NORTHWNDEntities ORM = new NORTHWNDEntities();

            //2. Load data from the DbSet into most of the available data structure in C#
            List<Customer> OutputList = new List<Customer>();
            foreach (Customer CustomerRecord in ORM.Customers.ToList())
            {
                if (CustomerRecord.ContactName != null && Regex.IsMatch(CustomerRecord.ContactName, custName, RegexOptions.IgnoreCase))
                {
                    OutputList.Add(CustomerRecord);
                }
            }

            ViewBag.CustomerList = OutputList;

            return View("CustomersView");
        }

    }
}