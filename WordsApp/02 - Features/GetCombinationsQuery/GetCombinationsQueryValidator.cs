using WordsApp.Data;
using WordsApp.Features;

public class GetCombinationsQueryValidator
{
    private List<string>? _errors = null;
    public List<string> Errors => _errors ?? [];

    // Normally probably would use FluentValidation but it seems overkill for now
    public GetCombinationsQueryValidator(IDataContext context, GetCombinationsRequest request)
    {
        if (!request.InputWords.Any(x => x.Length == context.SettingNumOfCharsInFullWord))
        {
            AddError($"No input word consisting of {context.SettingNumOfCharsInFullWord} characters was provided.");
        }

        if (request.InputWords.Count(x => x.Length == context.SettingNumOfCharsInFullWord) > 1)
        {
            AddError($"Multiple input words consisting of {context.SettingNumOfCharsInFullWord} characters were provided. Only one is allowed.");
        }

        var sumOfOtherPartsLengths = request.InputWords.Where(x => x.Length != context.SettingNumOfCharsInFullWord).Sum(x => x.Length);
        if (sumOfOtherPartsLengths != context.SettingNumOfCharsInFullWord)
        {
            AddError($"The total amount of characters for the word parts is '{sumOfOtherPartsLengths}', but expected '{context.SettingNumOfCharsInFullWord}'");
        }

        var nonLetterInputs = request.InputWords.Where(x => x.Any(c => !char.IsLetter(c))).ToList();
        if (nonLetterInputs.Any())
        {
            AddError($"The following word(s) contained one or more characters that are not letters: {string.Join(", ", nonLetterInputs)}");
        }
    }

    private void AddError(string error)
    {
        if (_errors == null)
        {
            _errors = new List<string>();
        }

        _errors.Add(error);
    }
}