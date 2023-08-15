using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sample_CRUD_Application.Model;

namespace Sample_CRUD_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select StudentID,FirstName,LastName,ContactNo,Email,DOB from
                            dbo.Students
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudAppDB");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Student student)
        {
            string query = @"
                           insert into dbo.Students
                           values (@FirstName,@LastName,@ContactNo,@Email,@DOB)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudAppDB");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    myCommand.Parameters.AddWithValue("@Email", student.Email);
                    myCommand.Parameters.AddWithValue("@DOB", student.DOB);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("{status:100}");
        }

        [HttpPut]
        public JsonResult Put(Student student)
        {
            string query = @"
                           update dbo.Students
                           set FirstName= @FirstName, 
                                LastName = @LastName, 
                                ContactNo = @ContactNo, 
                                Email = @Email, 
                                DOB = @DOB
                            where StudentID=@StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudAppDB");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", student.StudentID);
                    myCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", student.ContactNo);
                    myCommand.Parameters.AddWithValue("@Email", student.Email);
                    myCommand.Parameters.AddWithValue("@DOB", student.DOB);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Students
                            where StudentID=@StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudAppDB");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
