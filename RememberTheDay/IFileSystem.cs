namespace RememberTheDay
{
    public interface IFileSystem
    {
        string MakeFileName(string name);

        void SaveToFile(string filename, string[] lines);

        string[] GetFiles();

        string[] LoadFromFile(string filename);

    }
}