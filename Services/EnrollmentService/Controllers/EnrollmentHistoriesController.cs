using AutoMapper;
using EnrollmentService.Data;
using Entities.Concrete.EnrollmentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.EnrollmentDtos.EnrollmentHistoryDtos;

namespace EnrollmentService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentHistoriesController : ControllerBase
{
    private readonly ILogger<EnrollmentHistoriesController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public EnrollmentHistoriesController(ILogger<EnrollmentHistoriesController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/EnrollmentHistories
    [HttpGet]
    public async Task<IActionResult> GetEnrollmentHistoryAll()
    {
        var values = await _context.EnrollmentHistories.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Geçmiş Kayıt Bulunamadı");
            return NotFound("Böyle Bir Geçmiş Kayıt Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/EnrollmentHistories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEnrollmentHistory([FromBody]int id)
    {
        var enrollmentHistory = await _context.EnrollmentHistories.FindAsync(id);
        if (enrollmentHistory == null)
        {
            _logger.LogWarning("Böyle Bir Geçmiş Kayıt Bulunamadı");
            return NotFound("Böyle Bir Geçmiş Kayıt Bulunamadı");
        }
        return Ok(enrollmentHistory);
    }

    // POST: api/EnrollmentHistories
    [HttpPost]
    public async Task<IActionResult> CreateEnrollmentHistory([FromBody]CreateEnrollmentHistoryDto createEnrollmentHistoryDto)
    {
        var enrollmentHistory = _mapper.Map<EnrollmentHistory>(createEnrollmentHistoryDto);
        await _context.EnrollmentHistories.AddAsync(enrollmentHistory);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Geçmiş Kayıt Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/EnrollmentHistories/5
    [HttpPut]
    public async Task<IActionResult> UpdateEnrollmentHistory([FromBody]UpdateEnrollmentHistoryDto updateEnrollmentHistoryDto) 
    {
        var enrollmentHistory = _mapper.Map<EnrollmentHistory>(updateEnrollmentHistoryDto);
        _context.EnrollmentHistories.Update(enrollmentHistory);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Geçmiş Kayıt Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/EnrollmentHistories/5
    [HttpDelete]
    public async Task<IActionResult> DeleteEnrollmentHistory([FromBody]int id)
    {
        var enrollmentHistory = _context.EnrollmentHistories.Find(id);
        if (enrollmentHistory == null)
        {
            _logger.LogWarning("Böyle Bir Geçmiş Kayıt Bulunamadı");
            return NotFound("Böyle Bir Geçmiş Kayıt Bulunamadı");
        }
        _context.EnrollmentHistories.Remove(enrollmentHistory);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Geçmiş Kayıt Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}