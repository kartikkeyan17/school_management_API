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

namespace schoolManagement.Controllers
{
    public class staffController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        [HttpGet]
        [Route("api/staff/{staffID}")]
        public HttpResponseMessage getStudentsDataByID(string staffID)
        {
            try
            {
                var parameter = new
                {
                    staffID = staffID
                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var data = con.QueryFirstOrDefault<staff>("usp_GetStaffDetailByID", parameter, null, null, CommandType.StoredProcedure);

                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "There is no data present in this id:" + staffID);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [Route("api/staff/student/add")]
        public HttpResponseMessage postStudent([FromBody]student student)
        {
            try
            {
                var parameter = new
                {
                      studentID = student.studentID,

                      name =student.name,

                      gender =student.gender,

                     age    =student.age,

                    standard =student.standard,

                    fatherName =student.fatherName,

                    motherName =student.motherName,

                    contactNumber =student.contactNumber,

                    address =student.address
    };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Execute("usp_insertStudentData", parameter, null, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.Created, message);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/student/update/{studentID}")]
        public HttpResponseMessage putStudent([FromBody]student student, string studentID)
        {

            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("checkid", studentID);

                    parameter.Add("studentID ", student.studentID);

                    parameter.Add("name", student.name);

                    parameter.Add("gender ", student.gender);

                    parameter.Add("age ", student.age);

                    parameter.Add("standard", student.standard);

                    parameter.Add("fatherName", student.fatherName);

                    parameter.Add("motherName", student.motherName);

                    parameter.Add("contactNumber", student.contactNumber);

                    parameter.Add("address", student.address);


                    parameter.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);


                    con.Execute("usp_updateStudentData", parameter, null, null, CommandType.StoredProcedure);

                    int data = parameter.Get<int>("id");

                    if (data == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Provided id is invalid");
                    }


                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("api/student/delete/{studentID}")]
        public HttpResponseMessage deleteStaff(string studentID)
        {
            try
            {
                var parameter = new
                {
                    studentID = studentID

                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var data = con.Execute("usp_deleteStudentData", parameter, null, null, CommandType.StoredProcedure);

                    if (data == -1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);

                    }
                    else
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Please Valid id  provided");

                    }


                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
