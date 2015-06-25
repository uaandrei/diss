using Chess.Business.Interfaces;
using Chess.Infrastructure.Names;
using System.Collections.Generic;
namespace Chess.Business.ImplementationA.Rules
{
    public class RuleProvider : IRuleProvider
    {
        private Dictionary<string, IRule> _rules;
        public Dictionary<string, IRule> Rules { get { return _rules; } }

        public RuleProvider(IGameTable gameTable)
        {
            InitializeRules(gameTable);
        }

        private void InitializeRules(IGameTable gameTable)
        {
            _rules = new Dictionary<string, IRule>();
            _rules.Add(RuleNames.Mate, new MateRule(() => gameTable.GetFen()));
            _rules.Add(RuleNames.Chess, new ChessRule(gameTable));
        }
    }
}
