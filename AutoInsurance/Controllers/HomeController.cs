using AutoInsurance.Models;
using AutoInsurance.ViewModels;
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
        public ActionResult GetQuote(string FirstName, string LastName, string EmailAddress, 
            DateTime DateOfBirth,
            int CarYear, string CarMake, string CarModel, bool Dui, int Tickets, string Coverage)
        // these have to match the name attribute of the input form 
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(EmailAddress)
                || string.IsNullOrEmpty(CarMake) || string.IsNullOrEmpty(CarModel) || string.IsNullOrEmpty(Coverage))
            // i still need to account for the datetime, bool and int
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (BEInsuranceEntities db = new BEInsuranceEntities())
                {
                    double quote = 50;
                    var getQuote = new Customer();
                    getQuote.FirstName = FirstName;
                    getQuote.LastName = LastName;
                    getQuote.EmailAddress = EmailAddress;
                    getQuote.DateOfBirth = DateOfBirth;
                    getQuote.CarYear = CarYear;
                    getQuote.CarMake = CarMake;
                    getQuote.CarModel = CarModel;
                    getQuote.Dui = Dui;
                    getQuote.Tickets = Tickets;
                    getQuote.Coverage = Coverage;
                    // calculate quote
                    var today = DateTime.Now;
                    var customerAge = today.Year - DateOfBirth.Year;
                    if (DateOfBirth > today.AddYears(-customerAge)) customerAge--;
                    if (customerAge < 18)
                    {
                        quote += 100;
                    }
                    else if (customerAge < 25)
                    {
                        quote += 25;
                    }
                    else if (customerAge > 100)
                    {
                        quote += 25;
                    }
                    if (CarYear < 2000)
                    {
                        quote += 25;
                    }
                    else if (CarYear > 2015)
                    {
                        quote += 25;
                    }
                    if (CarMake.ToLower() == "porsche")
                    {
                        quote += 25;
                    }
                    if (CarMake.ToLower() == "porsche" && (CarModel.ToLower() == "911 carrera"))
                    {
                        quote += 25;
                    }
                    if (Tickets > 0)
                    {
                        quote += (Tickets * 10);
                    }
                    if (Dui == true)
                    {
                        quote = quote * 1.25;
                    }
                    if (Coverage== "full")
                    {
                        quote = quote * 1.5;
                    }
                    getQuote.Quote = quote;
                    db.Customers.Add(getQuote);
                    db.SaveChanges();
                }
                return RedirectToAction("Quote");
            }
        }

        public ActionResult Admin()
        {
            using (BEInsuranceEntities db = new BEInsuranceEntities())
            {
                var AllCustomers = db.Customers;
                //var CustomerList = new List<Customer>();

                var adminVms = new List<AdminVm>();
                foreach (var customer in AllCustomers)
                {
                    var adminVm = new AdminVm();
                    adminVm.FirstName = customer.FirstName;
                    adminVm.LastName = customer.LastName;
                    adminVm.EmailAddress = customer.EmailAddress;
                    adminVm.Quote = (double)customer.Quote;
                    adminVms.Add(adminVm);
                }
                return View(adminVms);
            }
        }

        public ActionResult Quote()
        {
            using (BEInsuranceEntities db = new BEInsuranceEntities())
            {
                var AllCustomers = db.Customers;
                //var CustomerList = new List<Customer>();

                var quoteVms = new List<QuoteVm>();
                foreach (var customer in AllCustomers)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.Id = customer.Id;
                    quoteVm.FirstName = customer.FirstName;
                    quoteVm.LastName = customer.LastName;
                    quoteVm.Quote = (double)customer.Quote;
                    if (quoteVm.Id == AllCustomers.Count())
                    {
                        quoteVms.Add(quoteVm);
                    }
                }
                return View(quoteVms);
            }
        }
    }
}