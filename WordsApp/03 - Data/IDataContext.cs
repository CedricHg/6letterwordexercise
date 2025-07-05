namespace WordsApp.Data;

public interface IDataContext
{
    IReadOnlyList<Word> Words { get; }
    int SettingNumOfCharsInFullWord { get; }
}