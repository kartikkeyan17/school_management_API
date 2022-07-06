using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolManagement.Models
{
    public class staff
    {
        

        public string staffID { get; set; }

        public string name { get; set; }

        public string gender { get; set; }

        public DateTime doj { get; set; }

        public int age { get; set; }

        public int expirence { get; set; }

        public string specialist { get; set; }

        public string qualification { get; set; }

        public int contactNumber{ get; set; }

        public string address { get; set; }
    }
}