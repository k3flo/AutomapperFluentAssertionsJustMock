using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAssertionsDemo.Models
{
    public class ExceptionThrower
    {
        public ExceptionThrower()
        {
            NoNullAllowedException innerException = new NoNullAllowedException("Null ist nicht erlaubt!");

            throw new FormatException("Fehlerhaftes Format!", innerException);
        }
    }
}
