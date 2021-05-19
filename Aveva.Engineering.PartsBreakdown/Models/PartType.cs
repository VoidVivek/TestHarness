// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartType.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the PartType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class representing each type of part for a part container
    /// </summary>
    public class PartType
    {
        /// <summary>
        /// Collection of Tags and number of exiting parts of this type.
        /// </summary>
        protected List<PartTagQuantity> partTagQuantities = new List<PartTagQuantity>();

        /// <summary>
        /// The quantity requested by user
        /// </summary>
        protected int requestedQuantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartType"/> class.
        /// </summary>
        /// <param name="baseClassName">Name of the base class.</param>
        /// <param name="baseClassUri">The base class URI.</param>
        /// <param name="actualClassName">Actual name of the class.</param>
        /// <param name="actualClassUri">The actual class URI.</param>
        /// <param name="association">The association.</param>
        public PartType(
            string baseClassName,
            string baseClassUri,
            string actualClassName,
            string actualClassUri,
            Association association)
        {
            BaseClassName = baseClassName;
            BaseClassUri = baseClassUri;
            ActualClassName = actualClassName;
            ActualClassUri = actualClassUri;
            Association = association;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PartType"/> class from being created.
        /// </summary>
        private PartType()
        {
        }

        /// <summary>
        /// Gets or sets the actual name of the class.
        /// </summary>
        /// <value>The actual name of the class.</value>
        public string ActualClassName { get; set; }

        /// <summary>
        /// Gets or sets the actual class URI.
        /// </summary>
        /// <value>The actual class URI.</value>
        public string ActualClassUri { get; set; }

        /// <summary>
        /// Gets or sets the association.
        /// </summary>
        /// <value>The association.</value>
        public Association Association { get; set; }

        /// <summary>
        /// Gets the name of the base class.
        /// </summary>
        /// <value>The name of the base class.</value>
        public string BaseClassName { get; private set; }

        /// <summary>
        /// Gets the base class URI.
        /// </summary>
        /// <value>The base class URI.</value>
        public string BaseClassUri { get; private set; }

        /// <summary>
        /// Gets the part tag quantities.
        /// </summary>
        /// <value>The part tag quantities.</value>
        public IEnumerable<PartTagQuantity> PartTagQuantities
        {
            get
            {
                return partTagQuantities;
            }
        }

        /// <summary>
        /// Gets or sets the requested quantity.
        /// </summary>
        /// <value>The requested quantity.</value>
        public virtual int RequestedQuantity
        {
            get
            {
                return requestedQuantity;
            }

            set
            {
                requestedQuantity = value;
            }
        }

        /// <summary>
        /// Adds the part tag quantity.
        /// </summary>
        /// <param name="partTagQuantity">The part tag quantity.</param>
        public void AddPartTagQuantity(PartTagQuantity partTagQuantity)
        {
            partTagQuantities.Add(partTagQuantity);
        }
    }
}