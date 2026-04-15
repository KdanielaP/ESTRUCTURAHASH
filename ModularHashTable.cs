using System;
using System.Collections.Generic;

namespace RegistroVacunas
{
    public class ModularHashTable : IRegistry
    {
        private const int TABLE_SIZE = 100;
        private List<Person>[] buckets = new List<Person>[TABLE_SIZE];

        public ModularHashTable()
        {
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                buckets[i] = new List<Person>();
            }
        }

        private int GetHash(string cui)
        {
            if (long.TryParse(cui, out long num))
            {
                return (int)(Math.Abs(num) % TABLE_SIZE);
            }
            return Math.Abs(cui.GetHashCode()) % TABLE_SIZE;
        }

        public void AddPerson(string cui, string name, List<Vaccine> vaccines)
        {
            var person = new Person(cui, name);
            foreach (var vac in vaccines)
            {
                person.AddVaccine(vac);
            }

            int index = GetHash(cui);
            var bucket = buckets[index];
            // Check if exists (no duplicates)
            if (!bucket.Exists(p => p.CUI == cui))
            {
                bucket.Add(person);
                Console.WriteLine($"Persona {name} (CUI: {cui}) registrada.");
            }
            else
            {
                Console.WriteLine("Persona ya existe.");
            }
        }

        public Person GetByCUI(string cui)
        {
            int index = GetHash(cui);
            return buckets[index].Find(p => p.CUI == cui);
        }

        public List<Person> GetAll()
        {
            var all = new List<Person>();
            foreach (var bucket in buckets)
            {
                all.AddRange(bucket);
            }
            return all;
        }

        public void Save(string filePath)
        {
            FileIO.Save(GetAll(), filePath);
        }

        public void Load(string filePath)
        {
            var persons = FileIO.Load(filePath);
            foreach (var p in persons)
            {
                AddPerson(p.CUI, p.Name, p.Vaccines);
            }
        }
    }
}

