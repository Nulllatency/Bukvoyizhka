using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bukvoyizhka.Interfaces
{
    public interface ICommandInterpreter
    {
        public void Interpret(string? text);
    }
}
