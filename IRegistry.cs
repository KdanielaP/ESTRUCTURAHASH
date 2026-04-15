using System.Collections.Generic;

namespace RegistroVacunas
{
    public interface IRegistry
    {
        void AddPerson(string cui, string name, List<Vaccine> vaccines);
        Person GetByCUI(string cui);
        List<Person> GetAll();
        void Save(string filePath);
        void Load(string filePath);
    }
}

