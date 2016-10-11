using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo.Interfaces
{
    public interface IAdd : ICalcOperation
    {
       
        int ValueA { get; set; }

        int ValueB { get; set; }
    }
}
