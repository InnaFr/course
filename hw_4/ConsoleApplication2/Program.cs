using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication2
{
   public class Program
    {
        public static void PerevodInOneBank(List<Bank> listBank)
        {
            Console.WriteLine("Введите название банка, между счетами которого вы хотите осуществить перевод");
            string nameBanka = Console.ReadLine();

            Bank bankN = null;
            foreach (var a in listBank)
            {
                if (a.name == nameBanka)
                {
                    bankN = a;
                }
            }
            if (bankN == null)
            {
                Console.WriteLine("Банк, услугами которого вы хотели воспользоваться, не существует");
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Введите ваши имя и фамилию");
            string nameHum = Console.ReadLine();

            Persons Human = null;
            foreach (var a in bankN.bankKlients)
            {
                if (a.firstName==nameHum)
                {
                    Human = a;
                }
            }

            if (Human==null)
            {
                Console.WriteLine("Человек, данные которого вы задали, не зарегистрирован в указанном банке");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.WriteLine("Укажите сумму перевода");
            int money = 0;
            try
            {
                money = int.Parse(Console.ReadLine());
            }
            catch (FormatException er)
            {
                Console.WriteLine("Ошибка: " + er.Message);
                Menu(listBank);
            }
            Console.WriteLine("Введите номер счета, с которого хотите списать деньги");
            string num_from = Console.ReadLine();
            Console.WriteLine("Введите номер счета, на который хотите перевести деньги");
            string num_where = Console.ReadLine();

            Human.Perevod(listBank, money, num_from, num_where, bankN);
        }

        public static void PerevodBetweenBanks(List<Bank> listBank)
        {
            int money = 0;
            string num_from = "";
            string num_where = "";
            Console.WriteLine("Укажите название банка, из которого хотите вывести средства");
            string bankFrom = Console.ReadLine();
            Console.WriteLine("Укажите название банка, в который вы хотите перевести средства");
            string bankWhere = Console.ReadLine();
                Bank strFrm = null;
                Bank strWhr = null;
                foreach (var a in listBank)
                {
                    if (a.name == bankFrom)
                    {
                        strFrm = a;
                    }
                    if (a.name == bankWhere)
                    {
                        strWhr = a;
                    }

                }
                if (strFrm == null || strWhr == null)
                {
                    if (strFrm == null)
                    {
                        Console.WriteLine("Банк, со счета которого вы хотите снять средства, не существует");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    if (strWhr == null)
                    {
                        Console.WriteLine("Банк, на счет которого вы хотите перевести средства, не существует");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            Console.WriteLine("Введите имя и фамилию владельца счета, с которого вы хотите списать средства");
            string nameOneHum = Console.ReadLine();
                Persons human = null;
                foreach (var a in strFrm.bankKlients)
                {
                    if (a.firstName == nameOneHum)
                    {
                        human = a;
                    }
                }
                if (human == null)
                {
                    Console.WriteLine("Клиент банка " + strFrm.name + " с такими данными не существует");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            Console.WriteLine("Укажите сумму перевода");
            try
            {
                money = int.Parse(Console.ReadLine());
            }
            catch (FormatException er)
            {
                Console.WriteLine("Ошибка: " + er.Message);
                Menu(listBank);
            }
            Console.WriteLine("Введите номер счета, с которого хотите списать деньги");
            num_from = Console.ReadLine();
            Console.WriteLine("Введите номер счета, на который хотите перевести деньги");
            num_where = Console.ReadLine();

            human.PerevodMezchduBanks(listBank, money, strFrm, strWhr, num_from, num_where);
            
   

        }

        public static void Menu(List<Bank> listBank)
        {
            Console.WriteLine("Выберите необходимое действие:");
            Console.WriteLine("1. Перевод средств в пределах одного банка");
            Console.WriteLine("2. Перевод средств между разными банками");
            Console.WriteLine("3. Выход из программы");
            int k = 0;
            try
            {
                k = int.Parse(Console.ReadLine());
            }
            catch(FormatException er)
            {
                Console.WriteLine("Ошибка: " + er.Message);
                Menu(listBank);
            }
            switch (k)
            {
                case 1:
                    {
                        PerevodInOneBank(listBank);
                        break;
                    }
                case 2:
                    {
                        PerevodBetweenBanks(listBank);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("До свидания!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Необходимо выбрать один из вариантов и указать его номер!");
                        Menu(listBank);
                        break;
                    }
            }
        }
        public static void Main(string[] args)
        {
            List<Bank> listBank = new List<Bank>();

            Bank bank = new Bank { name = "B" };
            Schet firsSchet = new Schet { numberScheta="0001", summa=1000};
            Schet secondSchet = new Schet { numberScheta="0002", summa=2000};
            Persons Ivanov = new Persons { firstName="Иванов Иван" };

            Bank bank_2 = new Bank { name = "S" };
            Schet secondBankSchet = new Schet { numberScheta="1000", summa=3000};
            Persons Petrov = new Persons { firstName = "Петров Петр" };

            bank.bankKlients.Add(Ivanov);
            bank.bankSchets.Add(firsSchet);
            bank.bankSchets.Add(secondSchet);
            Ivanov.personSchet.Add(firsSchet);
            Ivanov.personSchet.Add(secondSchet);
            firsSchet.Klients.Add(Ivanov);

            bank_2.bankKlients.Add(Petrov);
            bank_2.bankSchets.Add(secondBankSchet);

            secondBankSchet.Klients.Add(Petrov);
            Petrov.personSchet.Add(secondBankSchet);

            listBank.Add(bank);
            listBank.Add(bank_2);

            Menu(listBank);
           
            
            Console.ReadKey();

        }
    }
}
