// See https://aka.ms/new-console-template for more information

using Bukvoyizhka.CLI.HandleCommand;


string helloMessage = """"
    ---------------------------
    -stb - spin text body
    -slb - spin letters body
    -sts - spin text subject
    -sls - spin letters subject

    -sf=N - start from number of recipient 
    -mc=N - mutation chance 0-10

    recipient = address where mail was sent
    RECIPIENTS = path to file
    ---------------------------
    Commands:
        test *doesn't work* <ACCS> <recipient>  <MAIL> <subject>
        send <ACCS> <RECIPIENTS> <MAIL> <subject>
        q - quit program

    """";

Console.WriteLine(helloMessage);
try
{
    var interpreter = new CommandInterpreter();

string? commandHolder;

    do
    {
        commandHolder = Console.ReadLine();
        if (commandHolder == "q") break;
        interpreter.Interpret(commandHolder);
    }
    while (true);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}
Console.ReadKey();
