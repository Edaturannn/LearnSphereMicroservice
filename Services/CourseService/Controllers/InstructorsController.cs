using AutoMapper;
using CourseService.Data;
using Entities.Concrete.CourseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CourseDtos.InstructorDtos;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly ILogger<InstructorsController> _logger;
        private readonly IMapper _mapper;
        private readonly Context _context;

        public InstructorsController(ILogger<InstructorsController> logger, IMapper mapper, Context context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Instructors
        [HttpGet]
        public async Task<IActionResult> GetInstructorAll()
        {
            var values = await _context.Instructors.ToListAsync();
            if (values == null)
            {
                _logger.LogWarning("Böyle Bir Eğitmen Bulunamadı");
                return NotFound("Böyle Bir Eğitmen Bulunamadı");
            }

            return Ok(values);
        }

        // GET: api/Instructors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructor([FromBody] int id)
        {
            var course = await _context.Instructors.FindAsync(id);
            if (course == null)
            {
                _logger.LogWarning("Böyle Bir Eğitmen Bulunamadı");
                return NotFound("Böyle Bir Eğitmen Bulunamadı");
            }

            return Ok(course);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorDto createInstructorDto)
        {
            var instructor = _mapper.Map<Instructor>(createInstructorDto);
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Yeni Eğitmen Eklendi");
            return Ok("Başarılı Bir Şekilde Eklendi");
        }
        
        // PUT: api/Instructors/5
        [HttpPut]
        public async Task<IActionResult> UpdateInstructor([FromBody]UpdateInstructorDto updateInstructorDto) 
        {
            var instructor = _mapper.Map<Instructor>(updateInstructorDto);
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Eğitmen Güncellendi");
            return Ok("Başarılı Bir Şekilde Güncellendi");
        }

        // DELETE: api/Instructors/5
        [HttpDelete]
        public async Task<IActionResult> DeleteInstructor([FromBody]int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                _logger.LogWarning("Böyle Bir Eğitmen Bulunamadı");
                return NotFound("Böyle Bir Eğitmen Bulunamadı");
            }
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Eğitmen Silindi");
            return Ok("Başarılı Bir Şekilde Silindi");
        }
    }
}