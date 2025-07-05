// TOOD could use Spans for the various operations if we need to squeeze more performance out of this, at the possible expense of readability
public class Word
{
    public string Value { get; init; }

    public Word(string value)
    {
        Value = value;
    }

    public bool IsPossibleCombinationOf(IEnumerable<string> possibleParts)
    {
        string origValue = Value;
        foreach (var part in possibleParts)
        {
            if (!origValue.Contains(part, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }

    public string[] OrderParts(IEnumerable<string> parts)
    {
        return parts
            .Select(x => new
            {
                Part = x,
                Index = Value.IndexOf(x)
            })
            .OrderBy(x => x.Index)
            .Select(x => x.Part)
            .ToArray();
    }
}