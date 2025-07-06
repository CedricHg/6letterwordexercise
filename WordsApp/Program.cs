using WordsApp.Api;
using WordsApp.Data;


// This terminal is the API client essentially

Console.WriteLine("Pass in: desired full word (6 chars) and the word parts that should combine to form said word. Press ENTER for each word, pass empty line to indicate end of input:");

bool emptyLine;
List<string> inputWords = new List<string>();
do
{
    var line = Console.ReadLine()?.Trim();
    emptyLine = string.IsNullOrWhiteSpace(line);

    if (!emptyLine)
    {
        inputWords.Add(line!);
    }

} while (!emptyLine);

try
{
    var result = new WordsApi().GetCombinations(new WordsApp.Features.GetCombinationsRequest(inputWords));

    if (!result.Success)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"The application ran into the following error(s): {result.ErrorMessage}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();
        return;
    }

    if (result.Data == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Result.Data was null for some reason even though Result.Success is true. Talk about foobar!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();
        return;
    }

    var outputLine = $"{string.Join('+', result.Data.Parts)}={result.Data.FullWord}";
    Console.WriteLine(outputLine);
    Console.ReadKey();
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"An entirely unexpected exception occurred, namely: {e.Message} - stacktrace: {e.StackTrace}");
    Console.ForegroundColor = ConsoleColor.White;
    Console.ReadKey();
    return;
}
