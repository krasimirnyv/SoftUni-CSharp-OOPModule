using System;

namespace CommandPattern.Core.Contracts;

public class Engine : IEngine
{
    private readonly ICommandInterpreter commandInterpreter;
    public Engine(ICommandInterpreter command)
    {
        this.commandInterpreter = command;
    }

    public void Run()
    {
        while (true)
        {
            string input = Console.ReadLine();
            try
            {
                string result = commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid command!"); 
            }
        }
    }
}