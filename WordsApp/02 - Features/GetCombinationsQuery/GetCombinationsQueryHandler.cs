using WordsApp.Data;

namespace WordsApp.Features;

public class GetCombinationsQueryHandler(IDataContext context)
{
    public Result<GetCombinationsResponse> Handle(GetCombinationsRequest request)
    {
        try
        {
            return InnerHandle(request);
        }
        catch (Exception e)
        {
            return Result<GetCombinationsResponse>.Failure($"{e.Message} - STACKTRACE: {e.StackTrace}");
            // In production you'd just do this
            //return Result<GetCombinationsResponse>.Failure(e.Message);
        }
    }

    private Result<GetCombinationsResponse> InnerHandle(GetCombinationsRequest request)
    {
        // if we would use something like mediatr this validator would be part of the pipeline, keeping it simple now
        var validator = new GetCombinationsQueryValidator(context, request);
        if (validator.Errors.Any())
        {
            return Result<GetCombinationsResponse>.Failure(string.Join(", ", validator.Errors));
        }

        var desiredFullWord = new Word(request.InputWords.Single(x => x.Length == context.SettingNumOfCharsInFullWord));

        Console.WriteLine($"Desired full word: {desiredFullWord}");

        var otherParts = request.InputWords.Where(x => x != desiredFullWord.Value);
        var couldMatch = desiredFullWord.IsPossibleCombinationOf(otherParts);

        if (!couldMatch)
        {
            return Result<GetCombinationsResponse>.Failure("The passed in words are not sufficient to form a combination of the desired full word.");
        }

        var entities = context.Words.QueryMatching(otherParts);

        if (!otherParts.All(x => entities.Any(e => e.Value == x)))
        {
            return Result<GetCombinationsResponse>.Failure("Not every passed in word could be found in the list.");
        }

        // At this point we know we can combine all provided parts to form our desired word and that all parts are in the input.txt word list
        // Now we just need to put them in the right order
        var orderedParts = desiredFullWord.OrderParts(otherParts);

        return Result.Ok(new GetCombinationsResponse(
            orderedParts,
            desiredFullWord.Value));
    }
}