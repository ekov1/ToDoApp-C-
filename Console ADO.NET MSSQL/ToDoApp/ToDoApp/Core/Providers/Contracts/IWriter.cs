using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Core.Providers.Contracts
{
    public interface IWriter
    {
        void Write(string text);

        void WriteLine(string text);

    }
}
