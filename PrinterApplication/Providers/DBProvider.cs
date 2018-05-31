using PrinterApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterApplication.Providers
{
    public class DBProvider : IDBProvider
    {
        public int GetUserId()
        {
            //...connect to db and get user id;
            //return 1;
            throw new NotImplementedException();
        }
    
        public User GetUserById(int userId)
        {
            //...connect to db and get user;
            //return user;
            throw new NotImplementedException();
        }
    }
}
