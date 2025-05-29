using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IHistoryRepository
    {
        Task<ICollection<History>> GetAllHistoryAsync();
        Task<History> GetHIstoryByIdAsync(int id);
        Task AddNewHistoryAsync(History history);
        Task DeleteHistoryAsync(int id);

    }
}
