using Moq;

namespace RememberTheDay.UnitTests.DSL
{
    public class MacFileSystemBuilder
    {
        private readonly Mock<MacFileSystem> _fs;

        public MacFileSystemBuilder()
        {
            _fs = new Mock<MacFileSystem>();
        }
        
        public MacFileSystemBuilder IsEmpty()
        {
            _fs.Setup(x => x.GetFiles()).Returns(new string[0]);
            return this;            
        }

        public Mock<MacFileSystem> Please()
        {
            return _fs;
        }
    }
    
    class FileSystemStub : IFileSystem
    {
        public string MakeFileName(string name)
        {
            return "";
        }

        public void SaveToFile(string filename, string[] lines)
        {
            
        }

        public string[] GetFiles()
        {
            return new string[0];
        }

        public string[] LoadFromFile(string filename)
        {
            return new string[0];
        }       
    }    
    
}