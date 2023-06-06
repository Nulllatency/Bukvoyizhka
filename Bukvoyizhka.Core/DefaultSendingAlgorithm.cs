using Bukvoyizhka.Core.Mutator;
using Bukvoyizhka.Core.Credentials;
using Bukvoyizhka.Core.Interfaces;
using Bukvoyizhka.Core.Sender;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bukvoyizhka.Core
{
    public class DefaultSendingAlgorithm : ISendingAlgorithm
    {
        private readonly ILogger _logger;
        private List<string> _recipients;
        private List<GmailAccount> _accounts;
        private IMailMutator _mutator;

        public readonly int? RecipientsAmount;
        public bool IsCanRun { get; private set; } = false;
        public int SentMessages { get; private set; } 
        public int UsedAccounts { get; private set; }
        public int RecipientsHandled { get; private set; }
        

        public DefaultSendingAlgorithm(ILogger logger,IEnumerable<string> recipients, IEnumerable<GmailAccount> accounts, MailMutator mutator)
        {
            _logger = logger;          
            _recipients = recipients.ToList();            
            _accounts = accounts.ToList();
            _mutator = mutator;
            RecipientsAmount = _recipients.Count;

            Validate();
        }
        
        public void Run()
        {
            Validate();
            if(!IsCanRun) return;

            int sendingAttempts = 8;
            
            using var client = new GmailSmtpClient();

            var credsIndex = 0;

            var firstacc = _accounts[credsIndex];

            var creds = new NetworkCredential(firstacc.Address, firstacc.AppPassword);

            client.Credentials = creds;

            for (; RecipientsHandled < _recipients.Count; RecipientsHandled++)
            {               
                _mutator.Mail.To.Clear();
                _mutator.Mail.To.Add(new MailAddress(_recipients[RecipientsHandled]));

                _mutator.Mutate();
                try
                {

                    _mutator.Mail.From = new MailAddress(_accounts[credsIndex].Address);
                    client.Send(_mutator.Mail);
                    _logger.LogInformation($"from  {_accounts[credsIndex].Address}  to  {_recipients[RecipientsHandled]}");
                    SentMessages++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    sendingAttempts--;

                    if (!(credsIndex + 1 < _accounts.Count))
                    {
                        _logger.LogInformation("Accounts ended. Accounts used: " + _accounts.Count);
                        break;
                    }


                    if (sendingAttempts < 1)
                    {
                        UsedAccounts++;

                        credsIndex++;
                        sendingAttempts = 8;

                        creds.UserName = _accounts[credsIndex].Address;
                        creds.Password = _accounts[credsIndex].AppPassword;
                        client.Credentials = creds;
                    }
                }
                finally
                {
                    
                    
                }

            }

            
        }

        private void Validate()
        {
            IsCanRun = (_recipients != null
                && _accounts != null
                && _mutator.Mail != null)
                ||
                (_recipients?.Count > 0
                && _accounts?.Count > 0              
                );
        }
    }
}
