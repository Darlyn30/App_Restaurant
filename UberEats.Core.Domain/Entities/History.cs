using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats.Core.Domain.Entities
{
    public class History
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId {  get; set; }
        public DateTime DispatchedAt {  get; set; }
    }
}
