namespace WordsApp.Data;

public class DataContext : IDataContext
{
    private readonly string[] _words;

    public DataContext()
    {
        _words = File.ReadAllLines("input.txt");
    }

    public IReadOnlyList<string> Words => _words;
}