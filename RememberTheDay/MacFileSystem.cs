using System;
using System.IO;

namespace RememberTheDay
{
    public class MacFileSystem : IFileSystem
    {
        public string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        public virtual string MakeFileName(string name)
        {
            string[] parts;
            parts = name.Split(" "); 
            string filename = mydocpath + @"/person" + String.Join("_", parts) + ".txt";            
            return filename;
        } 
        
        public virtual void SaveToFile(string filename, string[] lines)
        {
            using (StreamWriter outputFile = new StreamWriter(filename)) {
                foreach (var line in lines)
                {
                    outputFile.WriteLine(line);    
                }
            }              
        }
        
        public virtual string[] GetFiles()
        {
            return Directory.GetFiles(mydocpath);
        }
        
        public virtual string[] LoadFromFile(string filename)
        {
            return File.ReadAllLines(filename);
        }        
    }}