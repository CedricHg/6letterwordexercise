using WordsApp.Data;

public class FakeDataContext : IDataContext
{
    public IReadOnlyList<Word> Words => [
        new Word("bar"),
        new Word("fo"),
        new Word("o"),
        new Word("icing"),
        new Word("d"),
        new Word("er"),
        new Word("wan"),
    ];

    public int SettingNumOfCharsInFullWord => 6;
}