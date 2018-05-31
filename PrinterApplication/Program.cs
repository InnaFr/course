using PrinterApplication.Logic;
using PrinterApplication.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbProvider = new DBProvider();
            var userId = dbProvider.GetUserId();
            var printer = new Printer(dbProvider);

            printer.PrintUserInfoById(userId);
            Console.ReadKey();
        }
    }
}
