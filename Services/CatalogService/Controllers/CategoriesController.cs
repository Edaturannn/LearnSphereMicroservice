using AutoMapper;
using CatalogService.Data;
using Entities.Concrete.CatalogService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CatalogDtos.CategoryDtos;

namespace CatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public CategoriesController(ILogger<CategoriesController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Categories
    [HttpGet]
    public async Task<IActionResult> GetCategoryAll()
    {
        var values = await _context.Categories.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Kategori Bulunamadı");
            return NotFound("Böyle Bir Kategori Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory([FromBody]int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            _logger.LogWarning("Böyle Bir Kategori Bulunamadı");
            return NotFound("Böyle Bir Kategori Bulunamadı");
        }
        return Ok(category);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Kategori Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/Categories/5
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryDto updateCategoryDto) 
    {
        var category = _mapper.Map<Category>(updateCategoryDto);
        _context.Categories.Update(category);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Kategori Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Categories/5
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromBody]int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            _logger.LogWarning("Böyle Bir Kategori Bulunamadı");
            return NotFound("Böyle Bir Kategori Bulunamadı");
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Kategori Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}