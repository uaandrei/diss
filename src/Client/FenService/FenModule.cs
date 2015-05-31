using FenService.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FenService
{
    [Module(ModuleName = Chess.Infrastructure.Names.ModuleNames.FenModule)]
    public class FenModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Initialize()
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            Container.RegisterType<IFenService, FenService>();
        }
    }
}
