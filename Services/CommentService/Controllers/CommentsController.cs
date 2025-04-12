using AutoMapper;
using CommentService.Data;
using Entities.Concrete.CatalogService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.CommentDtos.CommentDtos;
using Entities.Concrete.CommentService;

namespace CommentService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    private readonly HttpClient _httpClient;

    public CommentsController(ILogger<CommentsController> logger, IMapper mapper, Context context, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Comments
    [HttpGet]
    public async Task<IActionResult> GetCommentAll()
    {
        var values = await _context.Comments.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Yorum Bulunamadı");
            return NotFound("Böyle Bir Yorum Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Comments/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment([FromBody]int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            _logger.LogWarning("Böyle Bir Yorum Bulunamadı");
            return NotFound("Böyle Bir Yorum Bulunamadı");
        }
        return Ok(comment);
    }

    // POST: api/Comments
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody]CreateCommentDto createCommentDto)
    {
        var courseResponse = await _httpClient.GetAsync($"http://localhost:5170/api/Courses/{createCommentDto.CourseId}");
        if (!courseResponse.IsSuccessStatusCode)
        {
            return BadRequest("Geçersiz CourseId!");
        }

        createCommentDto.CreatedAt = DateTime.UtcNow;
        var comment = _mapper.Map<Comment>(createCommentDto);
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yeni Yorum Eklendi");
        return Ok("Başarılı Bir Şekilde Eklendi");
    }

    // PUT: api/Comments/5
    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody]UpdateCommentDto updateCommentDto) 
    {
        var comment = _mapper.Map<Comment>(updateCommentDto);
        _context.Comments.Update(comment);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Yorum Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Comments/5
    [HttpDelete]
    public async Task<IActionResult> DeleteComment([FromBody]int id)
    {
        var comment = _context.Comments.Find(id);
        if (comment == null)
        {
            _logger.LogWarning("Böyle Bir Yorum Bulunamadı");
            return NotFound("Böyle Bir Yorum Bulunamadı");
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Yorum Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}