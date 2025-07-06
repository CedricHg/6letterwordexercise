using System.Diagnostics.CodeAnalysis;

namespace WordsApp.Features;

public record GetCombinationsResponse(string[] Parts, string FullWord);

// Custom equality comparer to make tests more readable
public class GetCombinationsResponseEqualityComparer : IEqualityComparer<GetCombinationsResponse>
{
    public bool Equals(GetCombinationsResponse? x, GetCombinationsResponse? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }
        if (x is null || y is null)
        {
            return false;
        }

        if (x.FullWord != y.FullWord)
        {
            return false;
        }

        for (int i = 0; i < x.Parts.Length; i++)
        {
            if (x.Parts[i] != y.Parts[i])
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode([DisallowNull] GetCombinationsResponse obj)
    {
        if (obj is null) return 0;

        return HashCode.Combine(string.Join('+', obj.Parts), obj.FullWord);
    }
}