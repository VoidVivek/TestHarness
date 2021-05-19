// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPartsCommand.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the AddPartsCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Interop;

using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using Aveva.Core.Engineering.Interface;
using Aveva.Engineering.PartsBreakdown.Views;

namespace Aveva.Engineering.PartsBreakdown
{
    /// <summary>
    /// AddPartsCommand. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Aveva.ApplicationFramework.Presentation.Command" />
    public sealed class AddPartsCommand : Command
    {
        /// <summary>
        /// The command key.
        /// </summary>
        private const string CommandKey = "Aveva.Engineering.PartsBreakdown.AddPartsCommand";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPartsCommand"/> class.
        /// </summary>
        public AddPartsCommand()
        {
            this.Key = CommandKey;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            try
            {
                if (!DependencyResolver.Resolver.DependencyContainer.Exists<IAccessClassPicker>())
                {
                    DependencyResolver.Resolver.DependencyContainer.Register<IAccessClassPicker>(new Presentation.AccessEditors.AccessClassPicker());
                }
                var viewModel = new MainWindowViewModelFactory().Create();
                if (viewModel.IsSelectionValid())
                {
                    MainWindow win =
                        new MainWindow(viewModel) { WindowStartupLocation = WindowStartupLocation.CenterOwner };
                    viewModel.LoadData();
                    SetOwner(win);
                    win.ShowDialog();
                }
                else
                {
                    Console.WriteLine("Selected tags don't have same class", "Error");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Sets the owner for PartsBreakdown window.
        /// </summary>
        /// <param name="window">The window.</param>
        private void SetOwner(Window window)
        {
            var windowManager = DependencyResolver.GetImplementationOf<IWindowManager>();
            if (windowManager != null && windowManager.MainForm != null)
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                helper.Owner = windowManager.MainForm.Handle;
            }
        }
    }
}