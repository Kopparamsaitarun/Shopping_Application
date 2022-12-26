using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string IpAddress { get; set; }
    }
}
