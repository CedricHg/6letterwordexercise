using WordsApp.Data;
using WordsApp.Features;

namespace WordsApp.Api;

public class WordsApi
{
    private readonly IDataContext _dataContext;
    private bool isDevEnvironment = true; // TODO read from some appsetting
    
    public WordsApi(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public WordsApi()
    {
        _dataContext = new DataContext();
    }

    public Result<GetCombinationsResponse> GetCombinations(GetCombinationsRequest request)
    {
        try
        {
            return new GetCombinationsQueryHandler(_dataContext).Handle(request);
        }
        catch (Exception e)
        {
            // TODO move exception handling to an attribute or something so it's more implicit

            return isDevEnvironment
                ? Result<GetCombinationsResponse>.Failure($"{e.Message} - STACKTRACE: {e.StackTrace}")
                : Result<GetCombinationsResponse>.Failure(e.Message);
        }

    }
}