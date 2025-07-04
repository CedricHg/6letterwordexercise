using WordsApp.Features;

namespace WordsApp.Api;

public class WordsApiTests
{
    private readonly WordsApi _api;
    private readonly GetCombinationsResponseEqualityComparer _comparer;

    public WordsApiTests()
    {
        _api = new WordsApi();
        _comparer = new GetCombinationsResponseEqualityComparer();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void IsCorrectResponse(GetCombinationsRequest request, GetCombinationsResponse expectedResponse)
    {
        var response = _api.GetCombinations(request);
        // TODO writing a custom comparer is a neat exercise and all, but we don't really get much detailed information about the differences when the objects aren't equal, requiring debugging
        // better use something like FluentAssertions (or Shouldly if that supports Equivalent check by now..)
        Assert.True(_comparer.Equals(expectedResponse, response));
    }

    public static IEnumerable<TheoryDataRow<Features.GetCombinationsRequest, Features.GetCombinationsResponse>> TestData
    {
        get
        {
            yield return (new Features.GetCombinationsRequest(["foobar", "fo", "o", "bar"]), new Features.GetCombinationsResponse(["fo", "o", "bar"], "foobar"));
        }
    }
}