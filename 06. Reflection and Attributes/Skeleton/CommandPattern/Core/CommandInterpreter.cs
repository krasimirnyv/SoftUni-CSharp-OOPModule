using System;
using System.Linq;
using System.Reflection;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core;

public class CommandInterpreter : ICommandInterpreter
{
    public string Read(string args)
    {
        string[] arguments = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string commandName = arguments[0];

        string[] commandArgs = arguments.Skip(1).ToArray();

        Type commandType = Assembly
            .GetEntryAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name.Equals($"{commandName}Command", StringComparison.OrdinalIgnoreCase));
        
        ICommand commandInstance = Activator.CreateInstance(commandType) as ICommand;

        string result = commandInstance.Execute(commandArgs);

        return result;
    }
}