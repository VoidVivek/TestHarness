// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeMainWindowViewModelFactory.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the FakeMainWindowViewModelFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Aveva.Engineering.PartsBreakdown;
using Aveva.Engineering.PartsBreakdown.Repositories;
using Aveva.Engineering.PartsBreakdown.Services;
using Aveva.Engineering.PartsBreakdown.ViewModels;

namespace PartsBreakdown.TestHarness
{
    /// <summary>
    /// Class FakeMainWindowViewModelFactory.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.IMainWindowViewModelFactory" />
    internal class FakeMainWindowViewModelFactory : IMainWindowViewModelFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>creates and returns MainWindowViewModel.</returns>
        public MainWindowViewModel Create()
        {
            IPartsBreakdownRepository repository = new FakePartsBreakDownRepository();
            PartsBreakdownService service = new PartsBreakdownService(repository);
            return new MainWindowViewModel(service);
        }
    }

    /// <summary>
    /// Class FakeTmsMainWindowViewModelFactory.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.IMainWindowViewModelFactory" />
    internal class FakeTmsMainWindowViewModelFactory : IMainWindowViewModelFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>creates and returns MainWindowViewModelTms.</returns>
        public MainWindowViewModel Create()
        {
            IPartsBreakdownRepository repository = new FakePartsBreakDownRepository();
            PartsBreakdownService service = new PartsBreakdownService(repository);
            var fakeRequestFormViewModelCreator = new FakeRequestFormViewModelCreator();
            return new MainWindowViewModelTms(service, fakeRequestFormViewModelCreator);
        }
    }
}