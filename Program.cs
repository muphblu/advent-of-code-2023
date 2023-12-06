// See https://aka.ms/new-console-template for more information
using Spectre.Console;


var debugParam = AnsiConsole.Prompt(
    new TextPrompt<string>("[grey][[Optional]][/] Use [red]Debug[/] input?[grey]Y/[[N]][/]")
        .Validate(input => input.ToLower() == "y" || input.ToLower() == "n" || string.IsNullOrEmpty(input)
            ? ValidationResult.Success()
            : ValidationResult.Error("Type [bold]Y[/]es or [bold]N[/]o"))
        .AllowEmpty());
var isDebugInput = debugParam.ToLower() == "y";

var input = File.ReadAllLines(isDebugInput ? "./../../../debug.txt" : "./../../../input.txt");

Console.WriteLine(Task1B.Solve(input));