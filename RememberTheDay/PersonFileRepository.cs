using System;
using System.Collections.Generic;
using System.IO;

namespace RememberTheDay
{
    public class PersonFileRepository : IPersonRepo
    {
        private ILogger _logger;
        
        private string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        private string makeFileName(string name)
        {
            string[] parts;
            parts = name.Split(" "); 
            string filename = String.Join("_", parts);
            
            return mydocpath + @"/person" + filename + ".txt";
        }

        public void Add(Person p)
        {
            
            string[] lines = { p.Email, p.Name };
            _logger.Write("Person name: " + p.Name);
            using (StreamWriter outputFile = new StreamWriter(makeFileName(p.Name))) {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }        
        public List<Person> GetList()
        {

            string [] fileEntries = Directory.GetFiles(mydocpath);

            List<Person> persons = new List<Person>();
            
            foreach (var name in fileEntries)
            {
                if (name.Contains("person"))
                {
                    _logger.Write("found file: " + name);
                    string[] lines = System.IO.File.ReadAllLines(name);

                    if (lines.Length == 2)
                    {
                        _logger.Write("found Person - email: " + lines[0] + " name: " + lines[1]);
                        persons.Add(new Person(lines[0], lines[1]));    
                    }
                }
            }
            
            return persons;
        }
        
        public void Remove(Person p)
        {
            string filename = makeFileName(p.Name);
            File.Delete(filename);
        }        
    }
}