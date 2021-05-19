// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericTextBox.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the NumericTextBox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aveva.Engineering.PartsBreakdown.Views
{
    /// <summary>
    /// Class NumericTextBox, allows entry of numeric values only
    /// </summary>
    /// <seealso cref="System.Windows.Controls.TextBox" />
    public class NumericTextBox : TextBox
    {
        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> that contains the event data.</param>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}