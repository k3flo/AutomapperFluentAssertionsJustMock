using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustMockDemo.Interfaces;

namespace JustMockDemo.Models
{
    public class AddFunction : IAdd
    {
        public virtual object Calc()
        {
            return ValueA + ValueB;
        }

        public int ValueA { get; set; }
        public int ValueB { get; set; }
    }
}
