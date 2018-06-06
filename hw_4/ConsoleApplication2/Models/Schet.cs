using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Schet:Persons
    {
        public string numberScheta { get; set; }
        public int summa { get; set; }
        public List<Persons> Klients = new List<Persons>();
    }
}
