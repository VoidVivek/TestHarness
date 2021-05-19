// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePartsCommand.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the CreatePartsCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;

using Aveva.Engineering.PartsBreakdown.ViewModels;

namespace Aveva.Engineering.PartsBreakdown.Commands
{
    /// <summary>
    /// Class CreatePartsCommand.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    internal class CreatePartsCommand : ICommand
    {
        /// <summary>
        /// The main window view model
        /// </summary>
        private MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartsCommand"/> class.
        /// </summary>
        /// <param name="mainWindowViewModel">The main window view model.</param>
        public CreatePartsCommand(MainWindowViewModel mainWindowViewModel)
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
            // TODO: All validation checks
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            System.Windows.MessageBox.Show(
                "Show progress bar, in the background prepare a collection of requests to create parts and send the collection to Engineering API. On successful creation of parts close the progress bar");
        }
    }
}