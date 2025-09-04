using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Api.Data;
using StudentApp.Api.Data.Entities;

namespace StudentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext Context;

        public StudentController(AppDbContext dbContext)
        {
            Context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await Context.Students.ToListAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var student = await Context.Students.FirstOrDefaultAsync(p => p.Id == id);

            if (student is null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentEntity student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var num = await Context.Students
                .AnyAsync(s => s.studentNo == student.studentNo);

            if (num)
            {
                return Conflict(new { message = "Bu öğrenci numarası zaten kayıtlı." });
            }

            await Context.Students.AddAsync(student);
            await Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, [FromBody] StudentEntity student)
        {
            if (id != student.Id)
            {
                return BadRequest("ID eşleşmiyor.");
            }

            var numStudent = await Context.Students.FindAsync(id);
            if (numStudent is null)
            {
                return NotFound();
            }

            var exists = await Context.Students
                .AnyAsync(s => s.studentNo == student.studentNo && s.Id != id);

            if (exists)
            {
                return Conflict(new { message = "Bu öğrenci numarası başka bir öğrenciye ait." });
            }

            numStudent.studentName = student.studentName;
            numStudent.studentSurname = student.studentSurname;
            numStudent.studentNo = student.studentNo;
            numStudent.studentClass = student.studentClass;

            await Context.SaveChangesAsync();

            return Ok(numStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await Context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            Context.Students.Remove(student);

            await Context.SaveChangesAsync();

            return Ok();
        }
    }
}
