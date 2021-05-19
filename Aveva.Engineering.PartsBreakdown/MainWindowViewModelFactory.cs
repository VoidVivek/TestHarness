// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModelFactory.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   The main window view model factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Aveva.Engineering.PartsBreakdown.Repositories;
using Aveva.Engineering.PartsBreakdown.Services;
using Aveva.Engineering.PartsBreakdown.ViewModels;

namespace Aveva.Engineering.PartsBreakdown
{
    /// <summary>
    /// The main window view model factory.
    /// </summary>
    public class MainWindowViewModelFactory : IMainWindowViewModelFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="MainWindowViewModel"/>.
        /// </returns>
        public MainWindowViewModel Create()
        {
            PartsBreakdownService service = new PartsBreakdownService(new PartsBreakdownRepository());

            if (Info.EngineeringInfo.Instance.IsTMSProject)
            {
                return new MainWindowViewModelTms(service, new RequestFormViewModelCreator());
            }

            return new MainWindowViewModel(service);
        }
    }
}