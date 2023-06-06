using Bukvoyizhka.Core.Mutator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Bukvoyizhka.Core.Mutator.TextMutator;

namespace Bukvoyizhka.Core.Interfaces;
public interface ITextMutator
{
    public string Mutate(string text, params Spin[] spins);
}
