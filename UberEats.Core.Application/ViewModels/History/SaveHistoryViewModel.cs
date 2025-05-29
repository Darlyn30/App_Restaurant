using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Application.ViewModels.History
{
    public class SaveHistoryViewModel
    {
        public int HistoryId { get; set; }
        public int PaymentMethodId {  get; set; }
        public int UserId {  get; set; }
        public int CartId {  get; set; }

    }
}
