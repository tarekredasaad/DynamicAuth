using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Action
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string UserEmail { get; set; }
        public DateTime ActionTime { get; set; }
    }
}
