using BukvoyizhkaLibrary.Configs;
using BukvoyizhkaLibrary.Interfaces;
namespace BukvoyizhkaLibrary.Mutator
{
    public enum Spin
    {
        Text,
        Letters
    }
    public class TextMutator : ITextMutator
    {
        
        Dictionary<Spin, Func<string, string>> _spinAction;

        private ISpinner _spinText;
        private ISpinner _spinLetters;

        public TextMutator(MutatorsConfig config)
        {
            
            _spinText = new SpinText(config);
            _spinLetters = new SpinLetters(config);

            _spinAction = new Dictionary<Spin, Func<string, string>>()
            {
                [Spin.Text] = _spinText.Mutate,
                [Spin.Letters] = _spinLetters.Mutate
            };
        }

        public string Mutate(string text, params Spin[] spins)
        {
            foreach(var spin in spins)
            { 
                bool isExtacted = _spinAction.TryGetValue(spin, out var action);

                text = isExtacted ? action!(text) : text;
            }

            return text;
        }
        
    }
}
