using System;
using System.Collections.Generic;

namespace RegistroVacunas
{
    public class DictionaryRegistry : IRegistry
    {
        private Dictionary<string, Person> registry = new Dictionary<string, Person>();

        public void AddPerson(string cui, string name, List<Vaccine> vaccines)
        {
            if (registry.ContainsKey(cui))
            {
                Console.WriteLine("Persona ya existe.");
                return;
            }

            var person = new Person(cui, name);
            foreach (var vac in vaccines)
            {
                person.AddVaccine(vac);
            }
            registry[cui] = person;
            Console.WriteLine($"Persona {name} (CUI: {cui}) registrada con Dictionary.");
        }

        public Person GetByCUI(string cui)
        {
            registry.TryGetValue(cui, out Person person);
            return person;
        }

        public List<Person> GetAll()
        {
            return new List<Person>(registry.Values);
        }

        public void Save(string filePath)
        {
            FileIO.Save(GetAll(), filePath);
        }

        public void Load(string filePath)
        {
            registry.Clear();
            var persons = FileIO.Load(filePath);
            foreach (var p in persons)
            {
                AddPerson(p.CUI, p.Name, p.Vaccines);
            }
        }
    }
}

