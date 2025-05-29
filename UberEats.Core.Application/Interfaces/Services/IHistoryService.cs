using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberEats.Core.Application.ViewModels.History;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IHistoryService
    {
        Task<ICollection<HistoryViewModel>> GetAllHistory();
        Task<HistoryViewModel> GetHistoryById(int id);
        Task AddNewHistory(SaveHistoryViewModel historyVm);
        Task DeleteHistory(int id);
    }
}
