using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    [Module(ModuleName = Chess.Infrastructure.ModuleNames.GameModule)]
    public class GameModule : IModule
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
