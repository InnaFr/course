using PrinterApplication;
using PrinterApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace PrinterApplicationTests.Providers
{
    public class DBProviderFake : IDBProvider
    {

        public User GetUserById(int userId)
        {
            //var user = new User() { UserId = 10, FirstName = "FirstName", SecondName = "SecondName" };
            //var user = new Fixture().Create<User>();
            var user = new Fixture().Build<User>()
                                    .With(u => u.FirstName, "FirstName")
                                    .With(u => u.SecondName, "SecondName")
                                    .Create();
            return user;
        }
    }
}
