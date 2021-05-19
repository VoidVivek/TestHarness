// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the Class type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class to represent Data-Model Class.
    /// </summary>
    public class Class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="classUri">The class URI.</param>
        /// <param name="className">Name of the class.</param>
        public Class(string classUri, string className)
        {
            ClassUri = classUri;
            ClassName = className;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Class"/> class from being created.
        /// </summary>
        private Class()
        {
        }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the class URI.
        /// </summary>
        /// <value>The class URI.</value>
        public string ClassUri { get; set; }
    }
}