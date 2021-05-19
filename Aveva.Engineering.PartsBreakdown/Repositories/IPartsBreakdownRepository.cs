// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPartsBreakdownRepository.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the IPartsBreakdownRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Aveva.Engineering.PartsBreakdown.Models;

namespace Aveva.Engineering.PartsBreakdown.Repositories
{
    /// <summary>
    /// Interface IPartsBreakdownRepository
    /// </summary>
    public interface IPartsBreakdownRepository
    {
        /// <summary>
        /// Gets the part associations.
        /// </summary>
        /// <returns>Collection of Associations.</returns>
        IEnumerable<Association> GetPartAssociations();

        /// <summary>
        /// Gets the range class part types.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  PartTypes.</returns>
        IEnumerable<PartType> GetRangeClassPartTypes(Association association);

        /// <summary>
        /// Gets the selection class count.
        /// </summary>
        /// <returns>Integer value</returns>
        int GetSelectionClassCount();

        /// <summary>
        /// Gets the sub class part types from existing parts.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="subClassUris">The sub class uris.</param>
        /// <returns>Collection of  PartTypes.</returns>
        IEnumerable<PartType> GetSubClassPartTypesFromExistingParts(Association association, List<string> subClassUris);

        /// <summary>
        /// Gets the tag class counts.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  TagClassCounts.</returns>
        IEnumerable<TagClassCount> GetTagClassCounts(Association association);

        /// <summary>
        /// Filters out the class uris that don't have child classes.
        /// </summary>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns>List of classes that have child classes.</returns>
        IEnumerable<string> FilterClassUrisWithChildren(List<string> allRangeClassUris);

        /// <summary>
        /// Gets the sub class information.
        /// </summary>
        /// <param name="subClassUri">The sub class URI.</param>
        /// <param name="subClassName">Name of the sub class.</param>
        /// <param name="baseClassUri">The base class URI.</param>
        /// <param name="baseClassName">Name of the base class.</param>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns><c>true</c> if base class for sub class is found from allRangeClassUris, <c>false</c> otherwise.</returns>
        bool GetSubClassInfo(string subClassUri, out string subClassName, out string baseClassUri, out string baseClassName, List<string> allRangeClassUris);
    }
}