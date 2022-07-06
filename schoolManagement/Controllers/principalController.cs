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
    public class principalController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        [HttpGet]
        [Route("api/students/all")]
        public HttpResponseMessage getStudentsData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Query("usp_GetAllStudents", null, null, true, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/student/{studentID}")]
        public HttpResponseMessage getStudentsDataByID(string studentID)
        {
            try
            {
                var parameter = new
                {
                    studentid = studentID
                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    var data = con.QueryFirstOrDefault<principal>("usp_GetStudnetDetailByID", parameter, null, null, CommandType.StoredProcedure);

                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "There is no data present in this id:" + studentID);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/staffs")]
        public HttpResponseMessage getStaffSData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Query("usp_GetAllStaffs", null, null, true, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/staff/{staffID}")]
        public HttpResponseMessage getStaffsByID(string staffID )
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
                    var data = con.QueryFirstOrDefault<principal>("usp_GetStaffDetailByID", parameter, null, null, CommandType.StoredProcedure);

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
        [Route("api/staff/add")]
        public HttpResponseMessage postPrincipal([FromBody]staff staff)
        {
            try
            {
                var parameter = new
                {
                      staffID = staff.staffID,

                      name=staff.name,

                      gender =staff.gender,

                      doj =staff.doj,

                      age =staff.age,

                      expirence =staff.expirence,

                      specialist =staff.specialist,

                      qualification =staff.qualification,

                      contactNumber =staff.contactNumber,

                      address =staff.address
                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Execute("usp_insertStaffData", parameter, null, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.Created, message);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPut]
        [Route("api/staff/update/{staffID}")]
        public HttpResponseMessage putAdmin([FromBody]staff staff, string staffID)
        {

            try
            {
                
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("checkid ", staffID);

                    parameter.Add("staffID ",staff.staffID );

                    parameter.Add("name ",staff.name);

                    parameter.Add("gender ",staff.gender);

                    parameter.Add("doj ", staff.doj);

                    parameter.Add("age ", staff.age);

                    parameter.Add("expirence", staff.expirence);

                    parameter.Add("specialist", staff.specialist);

                    parameter.Add("qualification", staff.qualification);

                    parameter.Add("contactNumber", staff.contactNumber);

                    parameter.Add("address", staff.address);


                   parameter.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);


                    con.Execute("usp_updateStaffData", parameter, null, null, CommandType.StoredProcedure);

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
        [Route("api/staff/delete/{staffID}")]
        public HttpResponseMessage deleteStaff(string staffID)
        {
            try
            {
                var parameter = new
                {
                    checkid = staffID

                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var data = con.Execute("usp_deleteStaffData", parameter, null, null, CommandType.StoredProcedure);

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
