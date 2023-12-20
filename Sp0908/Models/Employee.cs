using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sp0908.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public int Sallary { get; set; }
        public String City { get; set; }
        public string Country { get; set; }
    }
}