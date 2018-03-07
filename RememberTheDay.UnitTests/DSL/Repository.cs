using Moq;

namespace RememberTheDay.UnitTests.DSL
{
    public class RepoBuilder
    {
        private IPersonRepo _repo;
        private Mock<MacFileSystem> _fs;
        private ILogger _logger = new LoggerStub();
        
        public RepoBuilder InMemory() 
        {
            _repo = new PersonMemoryRepository();
            return this;
        }

        public RepoBuilder InMacFileSystem()
        {
            _fs = The.MacFileSystem().Please();
            return this;
        }

        public RepoBuilder Empty()
        {
            _fs.Setup(x => x.GetFiles()).Returns(new string[0]);
            return this;
        }
        
        public RepoBuilder HasFile(string filename)
        {
            _fs.Setup(x => x.GetFiles()).Returns(new[] {"/path/to/personfile.txt"});
            return this;
        }
        
        public RepoBuilder InIt(string[] lines)
        {
            _fs.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new[] {"h.simpson@fox.com", "Homer", "1970/01/01"});

            return this;
        }

        public RepoBuilder Write(string[] lines)
        {
            _fs.Setup(x => x.SaveToFile(It.IsAny<string>(), new string[0]));
            return this;
        }

        public RepoBuilder ToFile(string filename)
        {
            _fs.Setup(x => x.MakeFileName(It.IsAny<string>())).Returns(filename);
            return this;
        }
        
        public RepoBuilder With(ILogger logger)
        {
            _logger = logger;
            return this;
        }
        
        public IPersonRepo Please()
        {
            return _repo ?? (_repo = new PersonRepository(_fs.Object, _logger));
        }
        
    }}