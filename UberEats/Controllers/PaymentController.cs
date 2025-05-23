using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var result = await _paymentService.GetAllPaymentMethods();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var result = await _paymentService.GetPaymentMethodById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPaymentMethod([FromBody] PaymentMethod paymentMethod)
        {
            await _paymentService.AddPaymentMethod(paymentMethod);
            return Ok(new { Message = "new payment method has been added" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] PaymentMethod payment)
        {
            await _paymentService.UpdatePaymentMethod(payment);
            return Ok(new { Message = "this payment method has been updated" });

        }

        [HttpDelete]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            await _paymentService.DeletePaymentMethod(id);
            return Ok(new { Message = "this payment method has been deleted" });
        }
    }
}
