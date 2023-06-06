using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bukvoyizhka.Core.Interfaces;
public interface IMailMutator
{
    public MailMessage Mail { get; }
    public MailMessage Mutate();
}
