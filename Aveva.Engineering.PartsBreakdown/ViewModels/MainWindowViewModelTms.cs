// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModelTms.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the MainWindowViewModelTms type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Aveva.Engineering.PartsBreakdown.Models;
using Aveva.Engineering.PartsBreakdown.Services;

namespace Aveva.Engineering.PartsBreakdown.ViewModels
{
    /// <summary>
    /// Class MainWindowViewModel for Tag Management System.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.ViewModels.MainWindowViewModel" />
    public class MainWindowViewModelTms : MainWindowViewModel
    {
        /// <summary>
        /// The request form view model creator
        /// </summary>
        private readonly IRequestFormViewModelCreator requestFormViewModelCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModelTms"/> class.
        /// </summary>
        /// <param name="partsBreakdownService">The parts breakdown service.</param>
        /// <param name="requestFormViewModelCreator">The request form view model creator.</param>
        public MainWindowViewModelTms(
            PartsBreakdownService partsBreakdownService,
            IRequestFormViewModelCreator requestFormViewModelCreator)
            : base(partsBreakdownService)
        {
            this.requestFormViewModelCreator = requestFormViewModelCreator;
        }

        /// <summary>
        /// Gets part type for a sub class of association range class.
        /// </summary>
        /// <param name="classUri">The class identifier.</param>
        /// <returns>PartType for classUri</returns>
        protected override PartType GetPartType(string classUri)
        {
            var partType = base.GetPartType(classUri);
            var partTypeTms = ConvertBasePartType(partType);

            partTypeTms.SetNamingViewModelCreator(this.requestFormViewModelCreator);

            return partTypeTms;
        }

        /// <summary>
        /// Sets the collection view.
        /// </summary>
        protected override void SetCollectionView()
        {
            ConvertBasePartTypes();
            foreach (var part in this.Parts)
            {
                ((PartTypeTms)part).SetNamingViewModelCreator(this.requestFormViewModelCreator);
            }

            base.SetCollectionView();
        }

        /// <summary>
        /// Converts the base part types.
        /// </summary>
        private void ConvertBasePartTypes()
        {
            var retVal = new List<PartType>();
            foreach (var partType in this.Parts)
            {
                var part = ConvertBasePartType(partType);
                retVal.Add(part);
            }

            this.Parts = retVal;
        }

        /// <summary>
        /// Converts base part type.
        /// </summary>
        /// <param name="partType">Type of the part.</param>
        /// <returns>Returns converted PartType.</returns>
        private PartTypeTms ConvertBasePartType(PartType partType)
        {
            var part = new PartTypeTms(
                partType.BaseClassName,
                partType.BaseClassUri,
                partType.ActualClassName,
                partType.ActualClassUri,
                partType.Association);
            part.TransferPartTagQuantities(partType);
            return part;
        }
    }
}