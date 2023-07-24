using Microsoft.AspNetCore.Mvc;
using WebApplicationtetsing.Models;
using WebApplicationtetsing.Services;

namespace WebApplicationtetsing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            return studentService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id) 
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }
            return student;
        }


        [HttpPost]
        public ActionResult<Student> Post(Student student) 
        {
            studentService.Create(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, Student student) {
        
            var existingStudent = studentService.Get(id);

            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }
            studentService.Update(id, existingStudent);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            studentService.Remove(student.Id);

            return Ok($"Student with Id = {id} deleted");
        }
    }
}
