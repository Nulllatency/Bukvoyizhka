using BukvoyizhkaLibrary.Interfaces;
using BukvoyizhkaLibrary.Mutator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static BukvoyizhkaLibrary.Mutator.TextMutator;
using static System.Net.Mime.MediaTypeNames;

namespace Bukvoyizhka.Core.Mutator;
public class MailMutator : IMailMutator
{
    private string _originalSubject;
    private string _originalBody;

    private MailMessage _mail;
    private ITextMutator _mutator;
    private Dictionary<string, Action> _argsSpinMap;
    private List<Spin> _spinArgsForSubject = new List<Spin>();
    private List<Spin> _spinArgsForBody = new List<Spin>();

    public MailMessage Mail { get { return _mail; } private set { _mail = value; } }

    public MailMutator(MailMessage mail, ITextMutator mutator, IEnumerable<string> spinsArgs)
    {
        _mail = mail;
        _originalSubject = mail.Subject;
        _originalBody = mail.Body;
        _mutator = mutator;

        _argsSpinMap = new Dictionary<string, Action>()
        {
            ["-stb"] = () => _spinArgsForBody.Add(Spin.Text),
            ["-slb"] = () => _spinArgsForBody.Add(Spin.Letters),
            ["-sts"] = () => _spinArgsForSubject.Add(Spin.Text),
            ["-sls"] = () => _spinArgsForSubject.Add(Spin.Letters)
        };

        foreach (var spinArgs in spinsArgs)
        {
            _argsSpinMap.TryGetValue(spinArgs, out var action);
            action?.Invoke();
        }
    }

    public MailMessage Mutate()
    {

        _mail.Subject = _mutator.Mutate(_originalSubject, _spinArgsForSubject.ToArray()) ?? _mail.Subject;
        _mail.Body = _mutator.Mutate(_originalBody, _spinArgsForBody.ToArray()) ?? _mail.Body;

        return _mail;
    }
}
