using AutoMapper;
using CourseService.Data;
using Entities.Concrete.CourseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CourseDtos.CourseDtos;

namespace CourseService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ILogger<CoursesController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public CoursesController(ILogger<CoursesController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Courses
    [HttpGet]
    public async Task<IActionResult> GetCourseAll()
    {
        var values = await _context.Courses.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Kurs Bulunamadı");
            return NotFound("Böyle Bir Kurs Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Courses/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            _logger.LogWarning("Böyle Bir Kurs Bulunamadı");
            return NotFound("Böyle Bir Kurs Bulunamadı");
        }

   
        
        return Ok(course);
    }

    // POST: api/Courses
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody]CreateCourseDto createCourseDto)
    {
        var course = _mapper.Map<Course>(createCourseDto);
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Kurs Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/Courses/5
    [HttpPut]
    public async Task<IActionResult> UpdateCourse([FromBody]UpdateCourseDto updateCourseDto) 
    {
        var course = _mapper.Map<Course>(updateCourseDto);
        _context.Courses.Update(course);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Kurs Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Courses/5
    [HttpDelete]
    public async Task<IActionResult> DeleteCourse([FromBody]int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            _logger.LogWarning("Böyle Bir Kurs Bulunamadı");
            return NotFound("Böyle Bir Kurs Bulunamadı");
        }
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Kurs Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}