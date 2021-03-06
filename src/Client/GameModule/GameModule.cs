﻿using Chess.Game.ViewModels;
using Chess.Game.Views;
using Chess.Persistance;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Chess.Game
{
    [Module(ModuleName = Chess.Infrastructure.Names.ModuleNames.GameModule)]
    public class GameModule : IModule
    {
        static GameModule()
        {
            PersistanceManager.Database = "chess";
        }

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
            Container.RegisterType<INotificationViewModel, NotificationViewModel>();
            Container.RegisterType<IMenuViewModel, MenuViewModel>();
            Container.RegisterType<IMoveHistoryViewModel, MoveHistoryViewModel>();
            Container.RegisterType<IOptionsViewModel, OptionsViewModel>();
            Container.RegisterType<IPromotionViewModel, PromotionViewModel>();
            Container.RegisterType<ILoginViewModel, LoginViewModel>();
            Container.RegisterType<ILoadSavedGameViewModel, LoadSavedGameViewModel>();
            Container.RegisterType<IView<IOptionsViewModel>, OptionsView>();
            Container.RegisterType<IView<IPromotionViewModel>, PromotionView>();
            Container.RegisterType<IView<ILoginViewModel>, LoginView>();
            Container.RegisterType<IView<ILoadSavedGameViewModel>, LoadSavedGameView>();
        }

        public static User LoggedUser { get; set; }
    }
}
