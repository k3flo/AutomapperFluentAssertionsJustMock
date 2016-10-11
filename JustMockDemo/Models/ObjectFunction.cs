using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustMockDemo.Interfaces;

namespace JustMockDemo.Models
{
    public class ObjectFunction : ICalcOperationWithParameter
    {
        public object Calc(object parameter)
        {
            //TODO: Hier tolle Logik implementieren

            return parameter;
        }
    }
}
