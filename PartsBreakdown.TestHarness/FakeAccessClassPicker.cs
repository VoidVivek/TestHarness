// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeAccessClassPicker.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Class FakeAccessClassPicker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows.Forms;

using Aveva.Core.Engineering.Interface;

namespace PartsBreakdown.TestHarness
{
    /// <summary>
    /// Class FakeAccessClassPicker.
    /// </summary>
    /// <seealso cref="Aveva.Core.Engineering.Interface.IAccessClassPicker" />
    class FakeAccessClassPicker : IAccessClassPicker
    {
        /// <summary>
        /// Gets SelectedClassId
        /// </summary>
        /// <value>The selected class identifier.</value>
        public string SelectedClassId { get; private set; }

        /// <summary>
        /// Method to LaunchClassPicker
        /// </summary>
        /// <param name="classIds">The class ids.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool LaunchClassPicker(List<string> classIds)
        {
            FakeClassPicker cp = new FakeClassPicker();
            cp.SetClassIds(classIds);
            var dialogResult = cp.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.SelectedClassId = "www.FakeClassId.Com";
                return true;
            }

            if (dialogResult == DialogResult.Retry)
            {
                this.SelectedClassId = "http://www.aveva.com/datamodel/item#bce01f86d-5d9a-450c-a35b-9dc951c7d68e";
                return true;
            }

            return false;
        }
    }
}