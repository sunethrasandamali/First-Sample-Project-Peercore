using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sample_CRUD_Application.Model;
using Sample_CRUD_Application.ViewModel;
using Sample_CRUD_Application.AppService;

namespace Sample_CRUD_Application.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentDataModel student)
        {
            _studentService.CreateStudent(student);
            return CreatedAtAction(nameof(GetAllStudents), new { id = student.StudentID }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int studentid, [FromBody] StudentDataModel student)
        {
            _studentService.UpdateStudent(studentid, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int studentid)
        {
            _studentService.DeleteStudent(studentid);
            return NoContent();
        }

    }
}
