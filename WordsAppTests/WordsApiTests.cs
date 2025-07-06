using WordsApp.Api;
using WordsApp.Features;

namespace WordsAppTests;

public class WordsApiTests
{
    private readonly WordsApi _api;
    private readonly GetCombinationsResponseEqualityComparer _comparer;

    public WordsApiTests()
    {
        _api = new WordsApi(new FakeDataContext());
        _comparer = new GetCombinationsResponseEqualityComparer();
    }

    [Theory]
    [MemberData(nameof(TestData))] // TODO apparently this shows all the test cases as 1 test, so if something fails it's kinda unclear what - inlinedata might be better
    public void IsCorrectResponse(GetCombinationsRequest request, GetCombinationsResponse expectedResponse)
    {
        var result = _api.GetCombinations(request);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        // TODO writing a custom comparer is a neat exercise and all, but we don't really get much detailed information about the differences when the objects aren't equal, requiring debugging
        // better use something like FluentAssertions (or Shouldly if that supports Equivalent check by now..)
        Assert.True(_comparer.Equals(expectedResponse, result.Data));
    }

    public static IEnumerable<TheoryDataRow<GetCombinationsRequest, GetCombinationsResponse>> TestData
    {
        get
        {
            yield return (new GetCombinationsRequest(["foobar", "o", "bar", "fo"]), new GetCombinationsResponse(["fo", "o", "bar"], "foobar"));
            yield return (new GetCombinationsRequest(["dicing", "icing", "d"]), new GetCombinationsResponse(["d", "icing"], "dicing"));
            yield return (new GetCombinationsRequest(["wander", "er", "d", "wan"]), new GetCombinationsResponse(["wan", "d", "er"], "wander"));
        }
    }
}