using Chess.Business.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Chess.Business.ImplementationA
{
    [Module(ModuleName = Chess.Infrastructure.Names.ModuleNames.ImplementationAModule)]
    public class ChessImplementationAModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            Container.RegisterType<IGameTable, GameTable>();
        }
    }
}
