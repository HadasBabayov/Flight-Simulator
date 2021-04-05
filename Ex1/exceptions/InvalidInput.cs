using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.exceptions
{
    class InvalidInput : Exception
    {
        public InvalidInput() { }
        public InvalidInput(string msg) : base(msg) { }

    }
}
