using System;
using ToDoApp.Core.Providers.Contracts;

namespace ToDoApp.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
