using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.MVVM.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int status { get; set; }
        public string StatusMessage { get; set; }
    }
}
