using AutoMapper;
using PaymentService.Data;
using Entities.Concrete.PaymentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dtos.Dtos.PaymentDtos.PaymentDtos;
using Shared.Events;
using PaymentService.MessageBus;

namespace PaymentService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly IMapper _mapper;
    private readonly Context _context;

    public PaymentsController(ILogger<PaymentsController> logger, IMapper mapper, Context context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }
    
    // GET: api/Payments
    [HttpGet]
    public async Task<IActionResult> GetPaymentAll()
    {
        var values = await _context.Payments.ToListAsync();
        if (values == null)
        {
            _logger.LogWarning("Böyle Bir Ödeme Kayıt Bulunamadı");
            return NotFound("Böyle Bir Ödeme Kayıt Bulunamadı");
        }
        return Ok(values);
    }

    // GET: api/Payments/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment([FromBody]int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            _logger.LogWarning("Böyle Bir Ödeme Kayıt Bulunamadı");
            return NotFound("Böyle Bir ödeme Kayıt Bulunamadı");
        }
        return Ok(payment);
    }

    // POST: api/Payments
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody]CreatePaymentDto payment)
    {
        payment.PaidAt = DateTime.UtcNow;
        payment.Status = "Completed";

        await _context.Payments.AddAsync(_mapper.Map<Payment>(payment));
        await _context.SaveChangesAsync();

        // Event yayınla
        var publisher = new PaymentEventPublisher();
        publisher.PublishPaymentCompleted(new PaymentCompletedEvent
        {
            UserId = payment.UserId,
            CourseId = payment.CourseId,
            PaidAt = payment.PaidAt
        });
        _logger.LogInformation("Ödeme Kayıt Eklendi");
        return Ok(payment);
    }
    // PUT: api/Payments/5
    [HttpPut]
    public async Task<IActionResult> UpdatePayment([FromBody]UpdatePaymentDto updatePaymentDto)
    {
        var payment = _mapper.Map<Payment>(updatePaymentDto);
        _context.Payments.Update(payment);
        await  _context.SaveChangesAsync();
        _logger.LogInformation("Ödeme Kayıt Güncellendi");
        return Ok("Başarılı Bir Şekilde Güncellendi");
    }

    // DELETE: api/Payments/5
    [HttpDelete]
    public async Task<IActionResult> DeletePayment([FromBody]int id)
    {
        var payment = _context.Payments.Find(id);
        if (payment == null)
        {
            _logger.LogWarning("Böyle Bir Ödeme Kayıt Bulunamadı");
            return NotFound("Böyle Bir Ödeme Kayıt Bulunamadı");
        }
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Ödeme Kayıt Silindi");
        return Ok("Başarılı Bir Şekilde Silindi");
    }
}