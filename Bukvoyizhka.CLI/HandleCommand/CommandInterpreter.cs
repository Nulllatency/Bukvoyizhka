using Bukvoyizhka.CLI.Commands;
using Bukvoyizhka.CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bukvoyizhka.CLI.HandleCommand
{
    public class CommandInterpreter : ICommandInterpreter
    {
        Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>()
        {
            ["test"] = new TestMail(),
            ["send"] = new SendToAll()
        };

        public void Interpret(string? text)
        {
            if (text is null) return;

            bool IsCommandFound = _commands.TryGetValue(text.Split(' ').FirstOrDefault(), out ICommand command);
            
            if(!IsCommandFound) return;

            var quotedArgs = Regex.Matches(text, "\"([^\"]*)\"")
                      .Cast<Match>()
                      .Select(m => m.Groups[1].Value)
                      .ToList().FirstOrDefault();

            // Remove arguments in quotes from the original string
            text = Regex.Replace(text, "\"([^\"]*)\"", "").Trim();

            var sortedArgsStartWithDash = text.Split(' ')
                .Where(arg => !string.IsNullOrWhiteSpace(arg))
                .Skip(1)
                .GroupBy(arg => arg.StartsWith("-"))
                .ToDictionary(group => group.Key, group => group.ToList());

            sortedArgsStartWithDash.TryGetValue(true, out var argsWithDash);
            sortedArgsStartWithDash.TryGetValue(false, out var args);


            if(quotedArgs is not null)
                args?.Add(quotedArgs);

            if (args is null || args.Count != 4)
                return;
            
            command!.Execute(args, argsWithDash);
            
        }
    }
}
