using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginPage page = new LoginPage(new Validator(), new Request(), new Logger());
            var res = page.DoLogin("guest", "1234567");
            Console.WriteLine("Logiin result {0}", res.ToString());
        }
    }
}
