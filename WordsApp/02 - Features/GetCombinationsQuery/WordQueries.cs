public static class WordQueries
{
    public static List<Word> QueryMatching(this IReadOnlyCollection<Word> words, IEnumerable<string> predicate)
    {
        return words.Where(x => predicate.Contains(x.Value)).ToList();
    }
}