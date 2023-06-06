using Bukvoyizhka.Core.Configs;
using Bukvoyizhka.Core.Credentials;
using Bukvoyizhka.Core.Mutator;
using Bukvoyizhka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Bukvoyizhka.CLI.Commands
{
    public class SendToAll : ICommand
    {
        public void Execute(List<string> args, List<string> argsWithDash)
        {
            var logger = new ConsoleLogger();

            var senders = File.ReadAllLines(args[0])
                .Select(line => line?.Split(":"))
                .Select(loginPass => new GmailAccount() { Address = loginPass?[0], AppPassword = loginPass?[1] })
                .ToList();
            var recipients = File.ReadAllLines(args[1]).ToList();
            var mailBody = File.ReadAllText(args[2]);
            var subject = args[3];

            MailMessage mail = new MailMessage() { Body = mailBody, Subject = subject };
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;


            var mConfig = new MutatorsConfig();
            var textMutator = new TextMutator(mConfig);
            var mailMutator = new MailMutator(mail: mail, textMutator, argsWithDash);
           

            var numericArgs = argsWithDash
                .Where(arg => args is not null && arg.StartsWith("-") && arg.Contains("="))
                .Select(arg => new KeyValuePair<string, int>(arg.Split('=')[0], int.Parse(arg.Split('=')[1])))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            bool IssfGot = numericArgs.TryGetValue("-sf", out var sfNumber);
            if (IssfGot)
                recipients = recipients.Skip(sfNumber).ToList();

            bool IsmcGot = numericArgs.TryGetValue("-mc", out var mcNumber);
            if(IsmcGot)
                mConfig.MutationChance = mcNumber;
           

            var algorithm = new DefaultSendingAlgorithm(
                logger: logger,
                recipients: recipients,
                accounts: senders,
                mutator: mailMutator
                );
            

            int sendingAttempts = 6;
            
            for(int i = 0; i < 6; i++)
            {
                algorithm.Run();
                logger.LogInformation($"Attempt {i+1} ended");
            }

            logger.LogInformation($"Sent messages: {algorithm.SentMessages}");
            logger.LogInformation($"Recps handled: {algorithm.RecipientsHandled}");
        }
    }
}
