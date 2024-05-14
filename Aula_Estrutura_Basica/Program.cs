using FluentColorConsole;

Console.WriteLine("Hello, World!");

var showMessage = new ShowMessage();
showMessage.Escrever();

var textLine = ColorConsole.WithBlueText;
textLine.WriteLine("Aprendendo a usar o NuGet");