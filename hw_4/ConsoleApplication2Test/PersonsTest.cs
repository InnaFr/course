using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication2;
using Moq;
using AutoFixture;
using System.Collections.Generic;

namespace ConsoleApplication2Test
{
    [TestClass()]
    public class PersonsTest
    {
        [TestMethod]
        public void Perevod_BankFromAndBankWhereExist_Test()
        {
            // Arrange
            string from = "123";
            string where = "124";
            Schet frm = null;
            Schet whr = null;

            Schet FirstSchet = new Schet()
            {
                numberScheta="123",
                summa=1000
            };
            Schet SecondScet = new Schet()
            {
                numberScheta="124",
                summa=2000
            };
            

            List<Schet> listSchets = new List<Schet>();

            listSchets.Add(FirstSchet);
            listSchets.Add(SecondScet);

            Bank bankTest = new Bank();

            bankTest = new Fixture().Build<Bank>().With(a=>a.bankSchets,listSchets).Without(a=>a.bankKlients).Create();

            //Act
            foreach (var a in bankTest.bankSchets)
            {
                if (a.numberScheta==from)
                {
                    frm = a;
                }
                if (a.numberScheta==where)
                {
                    whr = a;
                }
            }
            //Assert
            Assert.IsNotNull(whr);
            Assert.IsNotNull(frm);

        }


        public void PerevodMezchduBanks_Test()
        {
            // Arrange
            string numFrom = "123";
            string numWhere = "124";

            Schet FirstBankSchet = new Schet()
            {
                numberScheta = "123",
                summa = 1000
            };
            Schet SecondBankSchet = new Schet()
            {
                numberScheta = "124",
                summa = 2000
            };

            List<Schet> FirstBanklistSchets = new List<Schet>();
            List<Schet> SecondBanklistSchet = new List<Schet>();

            FirstBanklistSchets.Add(FirstBankSchet);
            SecondBanklistSchet.Add(SecondBankSchet);

            Bank FirstBank = new Bank()
            {
                bankSchets = FirstBanklistSchets
            };

            Bank SecondBank = new Bank()
            {
                bankSchets = SecondBanklistSchet
            };

            //Act
            var schetFr = new Mock<Persons>();
            var schetWhr = new Mock<Persons>();

            schetFr.Setup(a => a.schetReturn(FirstBank, numFrom));
            schetWhr.Setup(a => a.schetReturn(SecondBank, numWhere));

            //Assert

            Assert.IsNotNull(schetFr);
            Assert.IsNotNull(schetWhr);

        }
    
}
}
