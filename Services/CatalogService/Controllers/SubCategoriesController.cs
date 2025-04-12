using AutoMapper;
using CatalogService.Data;
using Entities.Concrete.CatalogService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CatalogDtos.SubCategoryDtos;

namespace CatalogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoriesController : ControllerBase
{
    private readonly ILogger<SubCategoriesController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public SubCategoriesController(ILogger<SubCategoriesController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/SubCategories
    [HttpGet]
    public async Task<IActionResult> GetSubCategoryAll()
    {
        var values = await _context.CourseTags.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Alt Kategory Bulunamadı");
            return NotFound("Böyle Bir Alt Kategory Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/SubCategories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubCategory([FromBody]int id)
    {
        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory == null)
        {
            _logger.LogWarning("Böyle Bir Alt Kategory Bulunamadı");
            return NotFound("Böyle Bir Alt Kategory Bulunamadı");
        }
        return Ok(subCategory);
    }

    // POST: api/SubCategories
    [HttpPost]
    public async Task<IActionResult> CreateSubCategory([FromBody]CreateSubCategoryDto createSubCategoryDto)
    {
        var subCategory = _mapper.Map<SubCategory>(createSubCategoryDto);
        await _context.SubCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Alt Kategori Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/SubCategories/5
    [HttpPut]
    public async Task<IActionResult> UpdateSubCategory([FromBody]UpdateSubCategoryDto updateSubCategoryDto) 
    {
        var subCategory = _mapper.Map<SubCategory>(updateSubCategoryDto);
        _context.SubCategories.Update(subCategory);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Alt Kategori Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/SubCategories/5
    [HttpDelete]
    public async Task<IActionResult> DeleteSubCategory([FromBody]int id)
    {
        var subCategory = _context.SubCategories.Find(id);
        if (subCategory == null)
        {
            _logger.LogWarning("Böyle Bir Alt Kategori Bulunamadı");
            return NotFound("Böyle Bir Alt Kategori Bulunamadı");
        }
        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Alt Kategori Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}