using Chess.Game.ViewModels;
using Chess.Infrastructure.Behaviours;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Chess.Game
{
    [Module(ModuleName = Chess.Infrastructure.Names.ModuleNames.GameModule)]
    public class GameModule : IModule
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            RegisterTypes();
            RegionManager.RegisterViewWithRegion(Chess.Infrastructure.Names.RegionNames.NotificationRegion, typeof(Views.NotificationView));
            RegionManager.RegisterViewWithRegion(Chess.Infrastructure.Names.RegionNames.MainRegion, typeof(Views.ChessTableView));
            RegionManager.RegisterViewWithRegion(Chess.Infrastructure.Names.RegionNames.SideRegion, typeof(Views.MoveHistoryView));
            RegionManager.RegisterViewWithRegion(Chess.Infrastructure.Names.RegionNames.MenuRegion, typeof(Views.MenuView));
        }

        private void RegisterTypes()
        {
            Container.RegisterType<IEventAggregator, EventAggregator>();
            Container.RegisterType<IChessSquareViewModel, ChessSquareViewModel>();
            Container.RegisterType<IChessTableViewModel, ChessTableViewModel>();
        }
    }
}
