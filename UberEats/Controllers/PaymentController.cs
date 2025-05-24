using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.PaymentMethod;
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

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentMethodById(int paymentId)
        {
            var result = await _paymentService.GetPaymentMethodById(paymentId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod([FromBody] SavePaymentMethodViewModel paymentVm)
        {
            await _paymentService.AddPaymentMethod(paymentVm);
            return Ok(new { Message = "New method has been added" } );
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] PaymentMethodViewModel vm)
        {
            await _paymentService.UpdatePaymentMethod(vm);
            return Ok(new { Message = "This method has been updated" } );
        }

        [HttpDelete("{paymentId}")]

        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            await _paymentService.DeletePaymentMethod(id);
            return Ok(new { Message = "This method has been deleted" });
        }

    }
}
