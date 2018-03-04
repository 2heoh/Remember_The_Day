using System;
using System.Collections.Generic;
using System.IO;

namespace RememberTheDay
{



    
    public class PersonRepository : IPersonRepo
    {
        public ILogger logger;
        public IFileSystem filesystem;
        
        public PersonRepository(ILogger log, IFileSystem fs)
        {
            logger = log;
            filesystem = fs;
        }           

        public void Add(Person p)
        {
            
            string[] lines = { p.Email, p.Name, p.BirthDay.ToString() };

            var filename = filesystem.MakeFileName(p.Name);
         
            logger.Write("filename: " + filename);
            
            filesystem.SaveToFile(filename, lines);
        }
        
        public List<Person> GetList()
        {

            var persons = new List<Person>();
            
            foreach (var name in filesystem.GetFiles())
            {
                if (!name.Contains("person")) continue;
                logger.Write("found file: " + name);
                var lines = filesystem.LoadFromFile(name);

                if (lines.Length != 3) continue;
                logger.Write(String.Format("found Person - email: {0} name: {1} birthday: {2}", lines));
                persons.Add(new Person(lines[0], lines[1], DateTime.Parse(lines[2])));
            }
            
            return persons;
        }
        
        public void Remove(Person p)
        {
            File.Delete(filesystem.MakeFileName(p.Name));
        }        
    }
}