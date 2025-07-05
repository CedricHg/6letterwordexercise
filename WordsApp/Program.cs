using WordsApp.Api;
using WordsApp.Data;

Console.WriteLine("Doing stuff");

// This terminal is the API client essentially
var response = new WordsApi(new DataContext()).GetCombinations(new WordsApp.Features.GetCombinationsRequest(["foobar", "fo", "o", "bar"]));
// TODO it seems weird to me that the user has to pass in the expected word "parts". Ideally you just pass in the desired full word and the application figures out which parts to use from the input
var outputLine = $"{string.Join('+', response.Parts)}={response.FullWord}";
Console.WriteLine(outputLine);