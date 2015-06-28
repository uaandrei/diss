using System.Collections.Generic;

namespace Chess.Business.Interfaces
{
    public interface IRuleProvider
    {
        Dictionary<string, IRule> Rules { get; }
    }
}
