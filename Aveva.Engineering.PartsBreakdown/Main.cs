using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveva.Engineering.PartsBreakdown
{
    using Aveva.ApplicationFramework;
    using Aveva.ApplicationFramework.Presentation;

    public class Main : IAddinInjected
    {
        private ICommandManager commandManager;

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Main()
        {
            this.Name = "Parts Breakdown";
            this.Description = "Addin that allows creation of Parts for a Tag";
        }

        public void Start(IDependencyResolver resolver)
        {
            commandManager = resolver.Exists<ICommandManager>() ? resolver.GetImplementationOf<ICommandManager>() : null;
            if (this.commandManager != null)
            {
                commandManager.Commands.Add(new AddPartsCommand());
            }
        }

        public void Start(ServiceManager serviceManager)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {

        }
    }
}
