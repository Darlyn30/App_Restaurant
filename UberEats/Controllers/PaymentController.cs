using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.DTO.Payment;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("simulate")]
        public async Task<IActionResult> Simulate([FromBody] PaymentRequestDTO dto)
        {
            try
            {
                var payment = await _paymentService.ProccessPayment(dto);
                return Ok(new
                {
                    message = payment.Status == "Approved" ? "Pago aprobado" : "Pago rechazado",
                    paymentId = payment.Id,
                    status = payment.Status
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
