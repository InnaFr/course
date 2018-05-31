using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterApplication.Logic
{
    public class Printer
    {
        private readonly IDBProvider dbprovider;
        public string lastTextWrittenInConsole;

        public Printer(IDBProvider dbprovider)
        {
            this.dbprovider = dbprovider;
        }

        public void PrintUserInfoById(int userId)
        {
            if (dbprovider != null)
            {
                var user = dbprovider.GetUserById(userId);
                var text = $"UserId: {user.UserId}, User name: {user.FirstName} {user.SecondName}";
                Console.WriteLine(text);
                lastTextWrittenInConsole = text;
            }
        }
    }
}
