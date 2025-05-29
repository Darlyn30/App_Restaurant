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
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationContext _context;
        public HistoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddNewHistoryAsync(History history)
        {
            var result = await GetHIstoryByIdAsync(history.Id);

            if (result != null)
                throw new Exception("este historial existe");

            await _context.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistoryAsync(int id)
        {
            var history = await GetHIstoryByIdAsync(id);

            if (history == null)
                throw new Exception("no se pudo encontrar dicho historial");

            _context.History.Remove(history);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<History>> GetAllHistoryAsync()
        {
            var historial = await _context.History
                .Include(p => p.PaymentMethod)
                .Include(u => u.User)
                .Include(c => c.Cart)
                    .ThenInclude(ci => ci.CartItems)
                .ToListAsync();

            return historial;
        }

        public async Task<History> GetHIstoryByIdAsync(int id)
        {
            var history = await _context.History
                .Include(p => p.PaymentMethod)
                .Include(u => u.User)
                .Include(c => c.Cart)
                    .ThenInclude(ci => ci.CartItems)
                .FirstOrDefaultAsync(hi => hi.Id == id);

            return history;
        }
    }
}
