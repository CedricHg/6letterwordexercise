using WordsApp.Data;
using WordsApp.Features;

namespace WordsAppTests;

public class GetCombinationsQueryValidatorTests
{
    private readonly IDataContext _context;

    public GetCombinationsQueryValidatorTests()
    {
        _context = new FakeDataContext();
    }

    private GetCombinationsRequest MakeRequest(params string[] words)
    {
        return new GetCombinationsRequest(words);
    }

    [Fact]
    public void Validator_NoFullWordLengthWord_AddsError()
    {
        var request = MakeRequest("abc", "de");
        var validator = new GetCombinationsQueryValidator(_context, request);

        Assert.Contains(validator.Errors, e => e.Contains("No input word consisting of 6 characters"));
    }

    [Fact]
    public void Validator_MultipleFullWordLengthWords_AddsError()
    {
        var request = MakeRequest("abcdef", "efghij");
        var validator = new GetCombinationsQueryValidator(_context, request);

        Assert.Contains(validator.Errors, e => e.Contains("Multiple input words consisting of 6 characters"));
    }

    [Fact]
    public void Validator_SumOfOtherPartsLengthsNotEqual_AddsError()
    {
        var request = MakeRequest("abcdef", "a", "b", "c");
        // "abcdef" is full word, "a", "b", "c" sum to 3, should be 6
        var validator = new GetCombinationsQueryValidator(_context, request);

        Assert.Contains(validator.Errors, e => e.Contains("The total amount of characters for the word parts is '3', but expected '6'"));
    }

    [Fact]
    public void Validator_NonLetterInputs_AddsError()
    {
        var request = MakeRequest("abcdef", "d3f", "g!h");
        var validator = new GetCombinationsQueryValidator(_context, request);

        Assert.Contains(validator.Errors, e => e.Contains("contained one or more characters that are not letters"));
        Assert.Contains("d3f", validator.Errors[0]);
        Assert.Contains("g!h", validator.Errors[0]);
    }

    [Fact]
    public void Validator_ValidInput_NoErrors()
    {
        var request = MakeRequest("abcdef", "abc", "de", "f");
        var validator = new GetCombinationsQueryValidator(_context, request);

        Assert.Empty(validator.Errors);
    }
}