using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Move;
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
