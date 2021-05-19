// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartTagQuantity.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the PartTagQuantity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class to show number of existing parts of a type for a given tag
    /// This is a content class of PartType class
    /// </summary>
    public class PartTagQuantity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartTagQuantity"/> class.
        /// </summary>
        /// <param name="tagNumber">The tag number.</param>
        /// <param name="existingQuantity">The existing quantity.</param>
        public PartTagQuantity(string tagNumber, int existingQuantity)
        {
            this.TagNumber = tagNumber;
            this.ExistingQuantity = existingQuantity;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PartTagQuantity"/> class from being created.
        /// </summary>
        private PartTagQuantity()
        {
        }

        /// <summary>
        /// Gets or sets the existing quantity.
        /// </summary>
        /// <value>The existing quantity.</value>
        public int ExistingQuantity { get; set; }

        /// <summary>
        /// Gets or sets the tag number.
        /// </summary>
        /// <value>The tag number.</value>
        public string TagNumber { get; set; }
    }
}