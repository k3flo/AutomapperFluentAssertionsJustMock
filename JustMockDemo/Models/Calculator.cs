using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustMockDemo.Interfaces;

namespace JustMockDemo.Models
{
    public class Calculator
    {
        public object Calculate(ICalcOperation function)
        {
            return function.Calc();
        }

        public object Calculate(ICalcOperationWithParameter function, object parameter)
        {
            return function.Calc(parameter);
        }
    }
}
