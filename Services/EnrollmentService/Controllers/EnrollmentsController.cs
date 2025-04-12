using AutoMapper;
using EnrollmentService.Data;
using Entities.Concrete.EnrollmentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.EnrollmentDtos.EnrollmentDtos;

namespace EnrollmentService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly ILogger<EnrollmentsController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    private readonly HttpClient _httpClient;

    public EnrollmentsController(ILogger<EnrollmentsController> logger, IMapper mapper, Context context, HttpClient httpClient)
    {
        _httpClient = httpClient;
       
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Enrollments
    [HttpGet]
    public async Task<IActionResult> GetEnrollmentsAll()
    {
        var values = await _context.Enrollments.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Kayıt Bulunamadı");
            return NotFound("Böyle Bir Kayıt Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Enrollments/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEnrollment([FromBody]int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            _logger.LogWarning("Böyle Bir Kayıt Bulunamadı");
            return NotFound("Böyle Bir Kayıt Bulunamadı");
        }
        return Ok(enrollment);
    }

    // POST: api/Enrollments
    [HttpPost]
    public async Task<IActionResult> CreateEnrollment([FromBody]CreateEnrollmentDto createEnrollmentDto)
    {
        // (Opsiyonel) Course ID doğrulama
    var courseResponse = await _httpClient.GetAsync($"http://localhost:5170/api/Courses/{createEnrollmentDto.CourseId}");
    if (!courseResponse.IsSuccessStatusCode)
    {
        return BadRequest("Geçersiz CourseId!");
    }

    var enrollment = _mapper.Map<Enrollment>(createEnrollmentDto);

    // Otomatik tarih ve durum
    enrollment.EnrollmentDate = DateTime.UtcNow;
    enrollment.Status = "Active";

    await _context.Enrollments.AddAsync(enrollment);
    await _context.SaveChangesAsync();

    _logger.LogInformation("Yeni Kayıt Eklendi");
    return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/Enrollments/5
    [HttpPut]
    public async Task<IActionResult> UpdateEnrollment([FromBody]UpdateEnrollmentDto updateEnrollmentDto) 
    {
        var enrollment = _mapper.Map<Enrollment>(updateEnrollmentDto);
        _context.Enrollments.Update(enrollment);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Kayıt Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Enrollments/5
    [HttpDelete]
    public async Task<IActionResult> DeleteEnrollment([FromBody]int id)
    {
        var enrollment = _context.Enrollments.Find(id);
        if (enrollment == null)
        {
            _logger.LogWarning("Böyle Bir Kayıt Bulunamadı");
            return NotFound("Böyle Bir Kayıt Bulunamadı");
        }
        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Kayıt Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}