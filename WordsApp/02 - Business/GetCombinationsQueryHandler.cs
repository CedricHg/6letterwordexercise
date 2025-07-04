namespace WordsApp.Business;

// TODO "GetCombinations" isn't really a great name, should be more like "GetParts" or something but don't wanna get bogged down in that atm
public class GetCombinationsQueryHandler
{
    public GetCombinationsResponse Handle(GetCombinationsRequest request)
    {
        return new GetCombinationsResponse(["fo", "o", "bar"], "foobar");
    }
}