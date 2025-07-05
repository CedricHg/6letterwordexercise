namespace WordsApp.Data;

public class DataContext : IDataContext
{
    private readonly Word[] _words;

    public DataContext()
    {
        var wordStrings = File.ReadAllLines("input.txt");
        _words = wordStrings.Select(x => new Word(x)).ToArray();
    }

    public IReadOnlyList<Word> Words => _words;
    public int SettingNumOfCharsInFullWord => 6;
}