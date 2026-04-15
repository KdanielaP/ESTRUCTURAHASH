using System;
using System.Collections.Generic;

namespace RegistroVacunas
{
    public class Person
    {
        public string CUI { get; set; }
        public string Name { get; set; }
        public List<Vaccine> Vaccines { get; set; } = new List<Vaccine>();

        public Person(string cui, string name)
        {
            CUI = cui;
            Name = name;
        }

        public void AddVaccine(Vaccine vaccine)
        {
            Vaccines.Add(vaccine);
        }

        public override string ToString()
        {
            var vacStr = string.Join(";", Vaccines);
            return $"{CUI}|{Name}|{vacStr}";
        }
    }
}

