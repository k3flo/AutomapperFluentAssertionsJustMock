using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo.Models
{
    public static class StaticClassWhichThrowsExceptionInConstructor
    {
        static StaticClassWhichThrowsExceptionInConstructor()
        {
            throw new Exception("Fehler!");
        }

        public static void DoSomTingWong()
        {
            
        }
    }
}
