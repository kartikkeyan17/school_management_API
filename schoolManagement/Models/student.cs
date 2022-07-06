using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schoolManagement.Models
{
    public class student
    {
       
        public string studentID { get; set; }

        public string name{ get; set; }

        public string gender { get; set; }

        public int age { get; set; }

        public int standard { get; set; }

        public string fatherName{ get; set; }

        public string motherName { get; set; }

        public int contactNumber { get; set; }

        public string address { get; set; }
    }
}