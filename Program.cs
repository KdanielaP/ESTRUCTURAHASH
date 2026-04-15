using System;
using System.Collections.Generic;

namespace RegistroVacunas
{
    class Program
    {
        private static IRegistry registry;
        private static readonly string FILE_PATH = @"c:/Users/danie/Desktop/RegistroVacunas/vacunas.txt";
        private static int variant = 0; // 1: Modular, 2: Dictionary

        static void Main(string[] args)
        {
            Console.WriteLine("=== Registro de Vacunas ===");
            SelectVariant();

            // Load existing data
            registry.Load(FILE_PATH);

            while (true)
            {
                ShowMenu();
                string option = Console.ReadLine();
                ProcessOption(option);
            }
        }

        private static void SelectVariant()
        {
            Console.WriteLine("Seleccione variante:");
            Console.WriteLine("1 - Hash Aritmética Modular (custom)");
            Console.WriteLine("2 - Dictionary de C#");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                registry = new ModularHashTable();
                variant = 1;
            }
            else
            {
                registry = new DictionaryRegistry();
                variant = 2;
            }
            Console.WriteLine($"Usando { (variant == 1 ? "Hash Modular" : "Dictionary") }");
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Registrar persona y vacunas");
            Console.WriteLine("2. Buscar persona por CUI");
            Console.WriteLine("3. Guardar en archivo");
            Console.WriteLine("4. Cambiar variante");
            Console.WriteLine("5. Salir");
        }

        private static void ProcessOption(string option)
        {
            switch (option)
            {
                case "1":
                    RegisterPerson();
                    break;
                case "2":
                    SearchByCUI();
                    break;
                case "3":
                    registry.Save(FILE_PATH);
                    break;
                case "4":
                    SelectVariant();
                    registry.Load(FILE_PATH);
                    break;
                case "5":
                    registry.Save(FILE_PATH);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        private static void RegisterPerson()
        {
            Console.Write("Ingrese CUI: ");
            string cui = Console.ReadLine();
            if (registry.GetByCUI(cui) != null)
            {
                Console.WriteLine("La persona ya existe.");
                return;
            }

            Console.Write("Ingrese nombre: ");
            string name = Console.ReadLine();

            var vaccines = new List<Vaccine>();
            while (true)
            {
                Console.Write("Ingrese nombre de vacuna (o 'fin' para terminar): ");
                string vacName = Console.ReadLine()?.Trim();
                if (vacName?.ToLower() == "fin") break;
                Console.Write("Ingrese fecha (dd/mm/yyyy): ");
                string date = Console.ReadLine();
                vaccines.Add(new Vaccine(vacName, date));
            }

            registry.AddPerson(cui, name, vaccines);
        }

        private static void SearchByCUI()
        {
            Console.Write("Ingrese CUI a buscar: ");
            string cui = Console.ReadLine();
            var person = registry.GetByCUI(cui);
            if (person == null)
            {
                Console.WriteLine("No existe la persona con CUI: " + cui);
            }
            else
            {
                Console.WriteLine($"Persona encontrada: {person.Name} (CUI: {person.CUI})");
                if (person.Vaccines.Count == 0)
                {
                    Console.WriteLine("No tiene vacunas registradas.");
                }
                else
                {
                    Console.WriteLine("Vacunas:");
                    foreach (var vac in person.Vaccines)
                    {
                        Console.WriteLine($"- {vac.Name} ({vac.Date})");
                    }
                }
            }
        }
    }
}

