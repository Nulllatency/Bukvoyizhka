using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bukvoyizhka.Commands
{
    public interface ICommand
    {
        void Execute(List<string> args, List<string> argsWithDash);
    }
}
