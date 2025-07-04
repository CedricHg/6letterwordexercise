namespace WordsApp.Data;

public interface IDataContext
{
    IReadOnlyList<string> Words { get; }
}