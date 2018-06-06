using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Persons:Bank
    {
        public string firstName { get; set; }
        public List<Schet> personSchet = new List<Schet>();

        public Schet schetReturn(Bank bank, string numSchet)
        {
            Schet schet = new Schet();
            foreach (var oi in bank.bankSchets)
            {
                if (oi.numberScheta == numSchet)
                {
                    schet = oi;
                }
            }

            return schet;
        }

        public void Perevod(List<Bank> listBank, int Money, string from, string where, Bank bank)
        {
            Schet frm = null;
            Schet whr = null;
            foreach (var a in bank.bankSchets)
            {
                if (a.numberScheta == from)
                {
                    frm = a;
                }
                if (a.numberScheta == where)
                {
                    whr = a;
                }
            }
            if (frm==null || whr==null)
            {
                if (frm==null)
                {
                   Console.WriteLine("Счет, с которого вы хотите вывести средства, не существует");
                   Console.ReadKey();
                   Program.Menu(listBank);
                }
                if (whr==null)
                {
                    Console.WriteLine("Счет, на который вы хотите перевести средства, не существует");
                    Console.ReadKey();
                    Program.Menu(listBank);
                }
            }
            else
            {
                if (frm.summa >= Money)
                {
                    whr.summa = whr.summa + Money;
                    frm.summa = frm.summa - Money;
                    Console.WriteLine("Сумма перевода: " + Money);
                    Console.WriteLine("Баланс исходного счета равен " + frm.summa);
                    Console.WriteLine("Баланс пополняемого счета равен " + whr.summa);
                    Program.Menu(listBank);
                }
                else
                {
                    Console.WriteLine("Недостатоно средств для перевода :(");
                    Console.ReadKey();
                    Program.Menu(listBank);
                }
            }

        }

        public void PerevodMezchduBanks(List<Bank> listBank, int Money, Bank BankFrom, Bank BankWhere, string numFrom, string numWhere)
        {
            Schet kFrom = schetReturn(BankFrom, numFrom);
            Schet kWhere = schetReturn(BankWhere, numWhere);

            if (kFrom==null || kWhere==null)
            {
                if (kFrom==null)
                {
                    Console.WriteLine("Счет, с которого вы хотите вывести средства, не существует");
                    Console.ReadKey();
                    Program.Menu(listBank);
                }
                if (kWhere==null)
                {
                    Console.WriteLine("Счет, на который вы хотите перевести средства, не существует");
                    Console.ReadKey();
                    Program.Menu(listBank);
                }
            }
            else
            {
                if (kFrom.numberScheta != kWhere.numberScheta)
                {
                    if (kFrom.summa >= Money)
                    {
                        kWhere.summa += Money;
                        kFrom.summa -= Money;

                        Console.WriteLine("Сумма перевода: " + Money);
                        Console.WriteLine("Баланс исходного счета равен " + kFrom.summa);
                        Console.WriteLine("Баланс пополняемого счета равен " + kWhere.summa);
                        Program.Menu(listBank);
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств для перевода :(");
                        Console.ReadKey();
                        Program.Menu(listBank);
                    }
                }
            }
            
            

        }
    }
}
