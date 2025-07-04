using WordsApp.Api;

Console.WriteLine("Doing stuff");

// This terminal is the API client essentially
var response = new WordsApi().GetCombinations(new WordsApp.Features.GetCombinationsRequest(["foobar", "fo", "o", "bar"]));
var outputLine = $"{string.Join('+', response.Parts)}={response.FullWord}";
Console.WriteLine(outputLine);