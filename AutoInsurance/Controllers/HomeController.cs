using AutoInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetQuote(string FirstName, string LastName, string EmailAddress/*, 
            DateTime DateOfBirth,
            int CarYear, string CarMake, string CarModel, bool Dui, int Tickets, string Coverage*/)
        // these have to match the name attribute of the input form 
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)) /*|| string.IsNullOrEmpty(EmailAddress)
                || string.IsNullOrEmpty(CarMake) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(Coverage))*/
            // i still need to account for the datetime, bool and int
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (BEInsuranceEntities db = new BEInsuranceEntities())
                {
                    var getQuote = new Customer();
                    getQuote.FirstName = FirstName;
                    getQuote.LastName = LastName;
                    getQuote.EmailAddress = EmailAddress;
                    /*getQuote.DateOfBirth = DateOfBirth;
                    getQuote.CarYear = CarYear;
                    getQuote.CarMake = CarMake;
                    getQuote.CarModel = CarModel;
                    getQuote.Dui = Dui;
                    getQuote.Tickets = Tickets;
                    getQuote.Coverage = Coverage;*/

                    db.Customers.Add(getQuote);
                    db.SaveChanges();
                }
                return View("Quote");
            }
        }


        public ActionResult Admin()
        {
            using (BEInsuranceEntities db = new BEInsuranceEntities())
            {
                var AllCustomers = db.Customers;
                var CustomerList = new List<Customer>();
            }
            
                return View("Admin");
        }
    }
}