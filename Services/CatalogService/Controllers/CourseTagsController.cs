using AutoMapper;
using CatalogService.Data;
using Entities.Concrete.CatalogService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CatalogDtos.CourseTagDtos;

namespace CatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseTagsController : ControllerBase
{
    private readonly ILogger<CourseTagsController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public CourseTagsController(ILogger<CourseTagsController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/CourseTags
    [HttpGet]
    public async Task<IActionResult> GetCourseTagAll()
    {
        var values = await _context.CourseTags.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Kurs Etiketi Bulunamadı");
            return NotFound("Böyle Bir Kurs Etiketi Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/CourseTags/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseTag([FromBody]int id)
    {
        var courseTag = await _context.CourseTags.FindAsync(id);
        if (courseTag == null)
        {
            _logger.LogWarning("Böyle Bir Kurs Etiketi Bulunamadı");
            return NotFound("Böyle Bir Kurs Etiketi Bulunamadı");
        }
        return Ok(courseTag);
    }

    // POST: api/CourseTags
    [HttpPost]
    public async Task<IActionResult> CreateCourseTag([FromBody]CreateCourseTagDto createCourseTagDto)
    {
        var courseTag = _mapper.Map<CourseTag>(createCourseTagDto);
        await _context.CourseTags.AddAsync(courseTag);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Kategori Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/CourseTags/5
    [HttpPut]
    public async Task<IActionResult> UpdateCourseTag([FromBody]UpdateCourseTagDto updateCourseTagDto) 
    {
        var courseTag = _mapper.Map<CourseTag>(updateCourseTagDto);
        _context.CourseTags.Update(courseTag);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Kategori Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/CourseTags/5
    [HttpDelete]
    public async Task<IActionResult> DeleteCourseTag([FromBody]int id)
    {
        var courseTag = _context.CourseTags.Find(id);
        if (courseTag == null)
        {
            _logger.LogWarning("Böyle Bir Kategori Bulunamadı");
            return NotFound("Böyle Bir Kategori Bulunamadı");
        }
        _context.CourseTags.Remove(courseTag);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Kategori Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}