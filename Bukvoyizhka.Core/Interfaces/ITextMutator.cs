using BukvoyizhkaLibrary.Mutator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static BukvoyizhkaLibrary.Mutator.TextMutator;

namespace BukvoyizhkaLibrary.Interfaces;
public interface ITextMutator
{
    public string Mutate(string text, params Spin[] spins);
}
