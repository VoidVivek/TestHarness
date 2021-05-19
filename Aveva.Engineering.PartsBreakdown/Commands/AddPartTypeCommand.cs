// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPartCommand.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the AddPartCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using Aveva.ApplicationFramework;
using Aveva.Core.Engineering.Interface;
using Aveva.Engineering.ClassModelManager;
using Aveva.Engineering.PartsBreakdown.ViewModels;

namespace Aveva.Engineering.PartsBreakdown.Commands
{
    /// <summary>
    /// Class AddPartCommand.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    internal class AddPartTypeCommand : ICommand
    {
        /// <summary>
        /// The main window view model
        /// </summary>
        private readonly MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPartTypeCommand"/> class.
        /// </summary>
        /// <param name="mainWindowViewModel">The main window view model.</param>
        public AddPartTypeCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            IAccessClassPicker accessClassPicker = DependencyResolver.Resolver.GetImplementationOf<IAccessClassPicker>();
            if (accessClassPicker != null)
            {
                List<string> classIds = mainWindowViewModel.GetBaseRangeClasses();
                if (accessClassPicker.LaunchClassPicker(classIds))
                {
                    if (!mainWindowViewModel.AddPartType(accessClassPicker.SelectedClassId))
                    {
                        MessageBox.Show("Part type for selected class already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}