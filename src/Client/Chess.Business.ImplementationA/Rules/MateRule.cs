using Chess.Business.Interfaces;
using Chess.Infrastructure.Communication;
using System;

namespace Chess.Business.ImplementationA.Rules
{
    public class MateRule : IRule
    {
        private Func<string> _getFen;

        public MateRule(Func<string> getFen)
        {
            _getFen = getFen;
        }

        public bool IsSatisfied()
        {
            return Gateway.IsMate(_getFen());
        }
    }
}
