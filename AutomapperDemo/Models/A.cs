using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperDemo.Models
{
    public class A : ABase
    {
        public A()
        {
            this.Name = "Mein Name";
            this.DateCreated = DateTime.Now;
            this.Value = 6.78m;
            this.Adresse = "Speicherstraße 49";
        }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Decimal Value { get; set; }
        public string Adresse { get; set; }

    }
}
