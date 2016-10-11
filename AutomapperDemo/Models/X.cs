using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperDemo.Models
{
    public class X : ABase
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}
