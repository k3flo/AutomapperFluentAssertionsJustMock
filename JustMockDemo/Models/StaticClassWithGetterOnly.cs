using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo.Models
{
    public static class StaticClassWithGetterOnly
    {
        private static string _value;

        static StaticClassWithGetterOnly()
        {
            _value = "StartWert";
        }

        public static string Value
        {
            get { return _value; }
        }
    }
}
