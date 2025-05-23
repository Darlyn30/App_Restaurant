using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;

        public PaymentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PaymentMethod paymentMethod)
        {
            var methodExist = await GetPaymentMethodByIdAsync(paymentMethod.Id);

            if (methodExist != null)
                throw new Exception("this method has already exist");

            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var method = await GetPaymentMethodByIdAsync(id);

            if (method == null)
                throw new Exception("the payment has not found");

            _context.PaymentMethods.Remove(method);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int id)
        {
            var method = await _context.PaymentMethods
                .FirstOrDefaultAsync(x => x.Id == id);

            return method;
        }

        public async Task UpdateAsync(PaymentMethod paymentMethod)
        {
            var method = await GetPaymentMethodByIdAsync(paymentMethod.Id);
            if (method == null)
                throw new Exception("this method cannot be found");

            //FIXME: PROBLEM WITH {Id}
            _context.Entry(paymentMethod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
