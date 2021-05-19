using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aveva.ApplicationFramework;

namespace PartsBreakdown.TestHarness
{
    public class Helper
    {
        public static IDependencyResolver Resolver()
        {
            DependencyContainer container = new DependencyContainer();
            IDependencyResolver dependencyResolver = new ApplicationFrameworkResolver(container);
            DependencyResolver.InitializeWith(dependencyResolver);
            return dependencyResolver;
        }
    }
}
