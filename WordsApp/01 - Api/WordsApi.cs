using WordsApp.Features;

namespace WordsApp.Api;

public class WordsApi
{
    public GetCombinationsResponse GetCombinations(GetCombinationsRequest request)
    {
        return new GetCombinationsQueryHandler().Handle(request);        
    }
}