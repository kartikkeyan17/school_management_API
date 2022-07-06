using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using schoolManagement.Models;
using schoolManagement.Controllers;
namespace schoolManagement.Controllers
{
    public class studentController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //[HttpGet]
        //[Route("api/studnet/{studentID}")]
        //public void getByID(string studentID)
        //{
        //    principalController p = new principalController();
        //    p.getStudentsDataByID(studentID);
        //}
    }
}
