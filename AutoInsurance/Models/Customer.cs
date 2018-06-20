using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoInsurance.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public bool Dui { get; set; }
        public bool Tickets { get; set; }
        public string Coverage { get; set; }
    }
}