using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Recording
    {
        public int ID;
        public string Title;
        public double Price;
        public string Date = DateTime.Now.ToShortDateString();
        public bool Add;
    }
}
