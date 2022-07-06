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
using System.Threading.Tasks;

namespace schoolManagement.Controllers
{
    public class adminController : ApiController
    {
        String cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        [HttpGet]
        [Route("api/admin")]
        public HttpResponseMessage getAdmin()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Query("usp_GetAdminDetails", null, null, true, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Principal")]
        public HttpResponseMessage getPrincipal()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Query("usp_GetPrincipalDetails",null, null,true,null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/principal/{principalid}")]
        public HttpResponseMessage getPrincipalByID(string principalid)
        {
            try
            {
                var parameter = new
                {
                    principalID = principalid
                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    var data = con.QueryFirstOrDefault<principal>("usp_GetPrincipalDetailByID", parameter, null,null,CommandType.StoredProcedure);
                   
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "There is no data present in this id:" + principalid);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK,data);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/principal/add")]
        public HttpResponseMessage postPrincipal([FromBody]principal principal )
        {
            try
            {
                var parameter = new
                {
                    principalID   = principal.principalID,
                    name          = principal.name,
                    gender        = principal.gender,
                    DOJ           = principal.DOJ,
                    age           = principal.age,
                    expirence     = principal.expirence,
                    education     = principal.education,
                    qualification = principal.qualification,
                    contactNumber = principal.contactNumber,
                    address       = principal.address


                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var message = con.Execute("usp_insertPrincipalData", parameter, null, null, CommandType.StoredProcedure);

                    return Request.CreateResponse(HttpStatusCode.Created);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/principal/update/{principalID}")]
        public HttpResponseMessage putPrincipal([FromBody]principal principal,string principalID)
        {
            try
            {
                //var parameter = new
                //{
                //    checkID     = principalID,
                //    principalID = principal.principalID,
                //    name = principal.name,
                //    gender = principal.gender,
                //    DOJ = principal.DOJ,
                //    age = principal.age,
                //    expirence = principal.expirence,
                //    education = principal.education,
                //    qualification = principal.qualification,
                //    contactNumber = principal.contactNumber,
                //    address = principal.address


                //};



                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var parameter = new DynamicParameters();

                    parameter.Add("checkID", principalID);

                    parameter.Add("principalID ", principal.principalID);

                    parameter.Add("name", principal.name);

                    parameter.Add("gender", principal.gender);

                    parameter.Add("DOJ", principal.DOJ);

                    parameter.Add("age", principal.age);

                    parameter.Add("expirence", principal.expirence);

                    parameter.Add("education", principal.education);

                    parameter.Add("qualification", principal.qualification);

                    parameter.Add("contactNumber", principal.contactNumber);

                    parameter.Add("address", principal.address);

                    parameter.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                     con.Execute("usp_updatePrincipalData", parameter, null, null, CommandType.StoredProcedure);

                    int data = parameter.Get<int>("@id");

                    if (data == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,data);
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

        [HttpPut]
        [Route("api/admin/update/{adminID}")]
        public HttpResponseMessage putAdmin([FromBody]admin admin, string adminID)
        {

            try
            {
                //var parameter = new
                //{
                //    id =   ParameterDirection.ReturnValue,
                //    checkid = adminID,
                //    adminID = admin.adminID,
                //    name = admin.name,
                //    gender = admin.gender,
                //    age = admin.age,
                //    contactNumber = admin.contactNumber,
                //    address = admin.address

                //};



                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@checkid ",adminID);

                    parameter.Add("@adminID ", admin.adminID);

                    parameter.Add("@name ", admin.name);

                    parameter.Add("@gender ", admin.gender);

                    parameter.Add("@age ", admin.age);

                    parameter.Add("@contactNumber", admin.contactNumber);

                    parameter.Add("@address", admin.address);

                    parameter.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);


                    con.Execute("usp_updateAdminData", parameter, null, null, CommandType.StoredProcedure);

                    int data = parameter.Get<int>("@id");
                    
                    if (data==1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,data);
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
        [Route("api/principal/delete/{principalID}")]
        public HttpResponseMessage deletePrinciple(string principalID)
        {
            try
            {
                var parameter = new
                {
                    checkid = principalID

                };

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    var data = con.Execute("usp_deletePrincipalData", parameter, null, null, CommandType.StoredProcedure);
                    
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
