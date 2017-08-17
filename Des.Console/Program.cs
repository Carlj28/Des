using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Implementation;

namespace Des.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new SubkeysWorker();
            x.GenerateSubkeys("133457799BBCDFF1");
        }
    }
}
