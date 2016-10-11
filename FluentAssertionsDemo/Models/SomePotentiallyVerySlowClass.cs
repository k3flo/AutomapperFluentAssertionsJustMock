using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAssertionsDemo.Models
{
    public class SomePotentiallyVerySlowClass
    {
        public void ExpensiveMethod()
        {
            for (short i = 0; i < short.MaxValue; i++)
            {
                string tmp = " ";
                if (!string.IsNullOrEmpty(tmp))
                {
                    tmp += " ";
                }
            }
        }
    }
}
