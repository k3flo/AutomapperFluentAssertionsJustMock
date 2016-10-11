using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperDemo.Models
{
    public class B : BBase
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Decimal Value { get; set; }
        public string Wohnort { get; set; }
    }
}
