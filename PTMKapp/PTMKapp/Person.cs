using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMKapp
{
    public class Person
    {
        public Person(string fio, string birthd, string gender, string p)
        {
            FIO = fio;
            BirthD = DateTime.Parse(birthd);
            Gender = gender;
            Years = int.Parse(p);
        }
        public Person(string fio, string birthd, string gender)
        {
            FIO = fio;
            BirthD = DateTime.Parse(birthd);
            Gender = gender;
        }
        public string FIO { get; set; }
        public DateTime BirthD { get; set; }
        public string Gender { get; set;}
        public int Years { get; set; }
    }
}
