using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegistroVacunas
{
    public static class FileIO
    {
        public static void Save(List<Person> persons, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var person in persons)
                {
                    writer.WriteLine(person.ToString());
                }
            }
            Console.WriteLine("Datos guardados en " + filePath);
        }

        public static List<Person> Load(string filePath)
        {
            var persons = new List<Person>();
            if (!File.Exists(filePath)) return persons;

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length < 2) continue;

                    var person = new Person(parts[0], parts[1]);
                    if (parts.Length > 2)
                    {
                        var vacParts = parts[2].Split(';');
                        foreach (var vacStr in vacParts)
                        {
                            if (vacStr.Contains('|'))
                            {
                                var vparts = vacStr.Split('|');
                                if (vparts.Length == 2)
                                {
                                    person.AddVaccine(new Vaccine(vparts[0], vparts[1]));
                                }
                            }
                        }
                    }
                    persons.Add(person);
                }
            }
            Console.WriteLine("Datos cargados desde " + filePath);
            return persons;
        }
    }
}

