using System;

namespace RegistroVacunas
{
    public class Vaccine
    {
        public string Name { get; set; }
        public string Date { get; set; }

        public Vaccine(string name, string date)
        {
            Name = name;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Name}|{Date}";
        }
    }
}

