using Chess.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
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
        private IRegionManager _regionManager;

        public GameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.ChessTableView));
        }
    }
}
