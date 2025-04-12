using AutoMapper;
using CatalogService.Data;
using Entities.Concrete.CatalogService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CatalogDtos.TagDtos;

namespace CatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public TagsController(ILogger<TagsController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Tags
    [HttpGet]
    public async Task<IActionResult> GetTagsAll()
    {
        var values = await _context.Tags.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Etiket Bulunamadı");
            return NotFound("Böyle Bir Etiket Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Tags/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTags([FromBody]int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            _logger.LogWarning("Böyle Bir Etiket Bulunamadı");
            return NotFound("Böyle Bir Etiket Bulunamadı");
        }
        return Ok(tag);
    }

    // POST: api/Tags
    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody]CreateTagDto createTagDto)
    {
        var tag = _mapper.Map<Tag>(createTagDto);
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Etiket Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/Tags/5
    [HttpPut]
    public async Task<IActionResult> UpdateSubCategory([FromBody]UpdateTagDto updateTagDto) 
    {
        var tag = _mapper.Map<Tag>(updateTagDto);
        _context.Tags.Update(tag);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Etiket Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Tags/5
    [HttpDelete]
    public async Task<IActionResult> DeleteTag([FromBody]int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null)
        {
            _logger.LogWarning("Böyle Bir Etiket Bulunamadı");
            return NotFound("Böyle Bir Etiket Bulunamadı");
        }
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Etiket Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}