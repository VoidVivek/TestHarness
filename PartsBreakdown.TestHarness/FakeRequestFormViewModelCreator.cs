// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRequestFormViewModelCreator.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the FakeRequestFormViewModelCreator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Aveva.Engineering.PartsBreakdown;
using Aveva.Engineering.TMS.Addin.ViewModel;

namespace PartsBreakdown.TestHarness
{
    /// <summary>
    /// Class FakeRequestFormViewModelCreator.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.IRequestFormViewModelCreator" />
    internal class FakeRequestFormViewModelCreator : IRequestFormViewModelCreator
    {
        /// <summary>
        /// Creates the specified class URI.
        /// </summary>
        /// <param name="classUri">The class URI.</param>
        /// <returns>Should return RequestFormViewModel, but not implemented.</returns>
        /// <exception cref="System.NotImplementedException">Not implemented</exception>
        public RequestFormViewModel Create(string classUri, string header)
        {
            throw new NotImplementedException();
        }
    }
}