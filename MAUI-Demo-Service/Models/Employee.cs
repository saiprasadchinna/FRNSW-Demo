using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo_Service.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        //public long contactNumber { get; set; }
        public int age { get; set; }
    }
}
