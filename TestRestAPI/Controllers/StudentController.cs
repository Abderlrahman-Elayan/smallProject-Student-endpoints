using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Data;
using TestRestAPI.Models;
using TestRestAPI.Models.DTOs;

namespace TestRestAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentController(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("Get All Students")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            var StudentsDTO = _mapper.Map<List<StudentDTO>>(students);
            var responseData = ApiResponse<List<StudentDTO>>.Ok(StudentsDTO, "Students Retrived Successfull");
            return Ok(responseData);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ApiResponse<StudentDTO>>> GetStuedentById(int? Id)
        {
            try
            {
                if (Id <= 0)
                {
                    var responseBody = ApiResponse<StudentDTO>.NotFound(message: "Student Id Must be greater than zero");
                    return NotFound(responseBody);
                }
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == Id);
                if (student == null)
                {
                    var responseBody = ApiResponse<StudentDTO>.NotFound(message: "Cant Find Student");
                    return NotFound(responseBody);
                }
                else
                {
                    var responseBody = ApiResponse<StudentDTO>.Ok(_mapper.Map<StudentDTO>(student), "Student Retrived Successfull");
                    return Ok(responseBody); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"an error occurred when retreiving Student with Id {Id}: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<StudentCreateDTO>> CreateStudent(StudentCreateDTO studentDto)
        {
            try
            {
                var responseBad= ApiResponse<StudentDTO>.BadRequest("inserted invalid Data");
                if (studentDto == null)
                {

                    return BadRequest(responseBad);
                }

                Student student = _mapper.Map<Student>(studentDto);

                await _context.AddAsync(student);
                await _context.SaveChangesAsync();
                var dto = _mapper.Map<StudentDTO>(student);
                var responseBody = ApiResponse<StudentDTO>.CreatedAt(dto,"Student Created Successfull");

                return CreatedAtAction(nameof(GetStuedentById), new { Id = student.Id }, responseBody);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"an error occurred when Creating student: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<StudentDTO>> UpdateStudent(int id, StudentUpdateDTO studentDto)
        {

            try
            {
                var responseBad = ApiResponse<StudentDTO>.BadRequest("Inserted invalid data");

                if (studentDto == null)
                {
                    return BadRequest(responseBad);
                }


                if (studentDto.Id != id)
                {
                    return BadRequest(responseBad);

                }
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
                var responseNotFound = ApiResponse<StudentDTO>.NotFound($"Student with id {id} not found");

                if (student == null)
                {
                    return NotFound(responseNotFound);
                }
                _mapper.Map(studentDto, student);

                await _context.SaveChangesAsync();
                var responseOk = ApiResponse<StudentUpdateDTO>.Ok(studentDto, "Student Updated Successfully");

                return Ok(responseOk);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"an error occurred when Creating student: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<StudentDTO>> DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s=>s.Id == id);
                
                if (student == null)
                {
                    return NotFound($"Student with id {id} not found");
                }
             

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"an error occurred when Creating student: {ex.Message}");
            }
        }


    }
}
