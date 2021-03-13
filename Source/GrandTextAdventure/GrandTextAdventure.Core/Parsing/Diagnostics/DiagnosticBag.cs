using System.Collections;
using System.Collections.Generic;
using GrandTextAdventure.Core.Parsers.EntityParser;
using GrandTextAdventure.Core.Parsing.Text;

namespace GrandTextAdventure.Core.Parsing.Diagnostics
{
    public sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

        public void ReportBadCharacter(TextSpan location, char character)
        {
            var message = $"Bad character input: '{character}'.";
            Report(location, message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
            Report(span, message);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _diagnostics.GetEnumerator();
        }

        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }
    }
}
