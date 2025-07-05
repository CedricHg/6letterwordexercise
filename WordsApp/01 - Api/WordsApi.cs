using WordsApp.Data;
using WordsApp.Features;

namespace WordsApp.Api;

public class WordsApi(IDataContext dataContext)
{
    public GetCombinationsResponse GetCombinations(GetCombinationsRequest request)
    {
        var result = new GetCombinationsQueryHandler(dataContext).Handle(request);

        if (result.Success)
        {
            return result.Data!;
        }

        throw new InvalidOperationException(result.ErrorMessage);
    }
}