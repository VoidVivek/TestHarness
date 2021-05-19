// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeClassPicker.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the FakeClassPicker type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PartsBreakdown.TestHarness
{
    /// <summary>
    /// Class FakeClassPicker.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FakeClassPicker : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeClassPicker"/> class.
        /// </summary>
        public FakeClassPicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the class ids.
        /// </summary>
        /// <param name="classIds">The class ids.</param>
        public void SetClassIds(List<string> classIds)
        {
            string str = string.Empty;
            foreach (string classId in classIds)
            {
                str += classId + Environment.NewLine;
            }

            this.txtBoxUris.Text = str;
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}