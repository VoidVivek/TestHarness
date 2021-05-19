using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveva.Engineering.PartsBreakdown
{
    using System.Windows;

    using Aveva.ApplicationFramework;
    using Aveva.Engineering.ClassModelManager;
    using Aveva.Engineering.NamingService;
    using Aveva.Engineering.TMS.Addin;
    using Aveva.Engineering.TMS.Addin.ViewModel;
    using Aveva.Engineering.TMS.Framework;
    using Aveva.Engineering.TMS.Framework.Client;
    using Aveva.Engineering.TMS.Framework.Model;

    public interface IRequestFormViewModelCreator
    {
        RequestFormViewModel Create(string classUri, string header);
    }

    public class RequestFormViewModelCreator : IRequestFormViewModelCreator
    {
        private DabaconDatabase database;

        private ViewModelFactory viewModelFactory;

        public RequestFormViewModelCreator()
        {
            IDataModelProvider dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            database = new DabaconDatabase();

            viewModelFactory = new ViewModelFactory(DependencyResolver.Resolver, dataModelProvider,
                NamingServiceManager.Instance.RuleStore, database, TMSFramework.Instance.InverseAssociationLocator);
        }

        public RequestFormViewModel Create(string classUri, string header)
        {
            var attributeValues = new List<DatabaseAttribute>();
            var requestFormViewModel = new RequestFormViewModel(RequestFormViewModel.RequestFormMode.Update, viewModelFactory, classUri,
                attributeValues: attributeValues);
            requestFormViewModel.GroupBoxHeader = header;
            requestFormViewModel.ShowCreateUpdateButton = requestFormViewModel.ShowCancelButton = Visibility.Collapsed;
            return requestFormViewModel;
        }
    }
}
