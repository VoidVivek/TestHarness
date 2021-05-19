// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Association.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the Association type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class to represent Data-Model Association.
    /// </summary>
    public class Association
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Association"/> class.
        /// </summary>
        /// <param name="associationUri">The association URI.</param>
        /// <param name="associationName">Name of the association.</param>
        /// <param name="attributeNames">The attribute names.</param>
        /// <param name="rangeClasses">The range classes.</param>
        /// <param name="cardinality">The cardinality.</param>
        public Association(
            string associationUri,
            string associationName,
            IEnumerable<string> attributeNames,
            IEnumerable<Class> rangeClasses,
            string cardinality)
        {
            this.AssociationUri = associationUri;
            this.AssociationName = associationName;
            this.AttributeNames = attributeNames;
            this.RangeClasses = rangeClasses;
            this.Cardinality = cardinality;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Association"/> class from being created.
        /// </summary>
        private Association()
        {
        }

        /// <summary>
        /// Gets or sets the name of the association.
        /// </summary>
        /// <value>The name of the association.</value>
        public string AssociationName { get; set; }

        /// <summary>
        /// Gets or sets the association URI.
        /// </summary>
        /// <value>The association URI.</value>
        public string AssociationUri { get; set; }

        /// <summary>
        /// Gets or sets the association attribute(s).
        /// </summary>
        /// <value>The attribute names.</value>
        public IEnumerable<string> AttributeNames { get; set; }

        /// <summary>
        /// Gets or sets the cardinality.
        /// </summary>
        /// <value>The cardinality.</value>
        public string Cardinality { get; set; }

        /// <summary>
        /// Gets or sets the range classes.
        /// </summary>
        /// <value>The range classes.</value>
        public IEnumerable<Class> RangeClasses { get; set; }
    }
}