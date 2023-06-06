using BukvoyizhkaLibrary;
using BukvoyizhkaLibrary.Configs;
using BukvoyizhkaLibrary.Credentials;
using BukvoyizhkaLibrary.Mail;
using BukvoyizhkaLibrary.Mutator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bukvoyizhka.Commands
{
    public class TestMail : ICommand
    {
        

        public void Execute(List<string> args, List<string> argsWithDash)
        {
            //    var logger = new ConsoleLogger();

            //    var senders = File.ReadAllLines(args[0])
            //        .Select(line => line.Split(":"))
            //        .Select(loginPass => new GmailAccount() { Address = loginPass[0], AppPassword = loginPass[1] })
            //        .ToList();            
            //    var recipient = new List<string>() { args[1] };
            //    var mailBody = File.ReadAllText(args[2]);
            //    var subject = args[3];

            //    MailMessage mail = new MailMessage() { Body = mailBody, Subject = subject };
            //    mail.SubjectEncoding = Encoding.UTF8;
            //    mail.BodyEncoding = Encoding.UTF8;

            //    var mConfig = new MutatorsConfig();
            //    var textMutator = new TextMutator(mConfig);
            //    var mailMutator = new MailMutator(mail: mail, textMutator, argsWithDash);

            //    Dictionary<string, Action> argsSpinMap = new Dictionary<string, Action>()
            //    {
            //        ["-stb"] = mailMutator.AddSpinTextBody,
            //        ["-slb"] = mailMutator.AddSpinLettersBody,
            //        ["-sts"] = mailMutator.AddSpinTextSubject,
            //        ["-sls"] = mailMutator.AddSpinLettersSubject                
            //    };

            //    var numericArgs = argsWithDash
            //       .Where(arg => arg.StartsWith("-") && arg.Contains("="))
            //       .Select(arg => new KeyValuePair<string, int>(arg.Split('=')[0], int.Parse(arg.Split('=')[1])))
            //       .ToDictionary(pair => pair.Key, pair => pair.Value);         

            //    bool IsmcGot = numericArgs.TryGetValue("-mc", out var mcNumber);
            //    if (IsmcGot)
            //        mConfig.MutationChance = mcNumber;

            //    foreach (var dashArg in argsWithDash)
            //    {
            //        argsSpinMap.TryGetValue(dashArg, out var action);
            //        action?.Invoke();
            //    }

            //    var algorithm = new DefaultSendingAlgorithm(
            //        logger: logger,
            //        recipients: recipient,
            //        accounts: senders,
            //        message: mail
            //        );

            //    algorithm.Run();

            //    logger.LogInformation($"Sent messages: {algorithm.SentMessages}");
            //    logger.LogInformation($"Recps handled: {algorithm.RecipientsHandled}");
        }
    }
}
