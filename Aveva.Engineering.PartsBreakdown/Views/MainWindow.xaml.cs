// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Aveva.Engineering.PartsBreakdown.ViewModels;

namespace Aveva.Engineering.PartsBreakdown.Views
{
    /// <summary>
    /// Class MainWindow.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="mainWindowViewModel">The main window view model.</param>
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }

        /// <summary>
        /// Handles the Click event of the Cancel Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}