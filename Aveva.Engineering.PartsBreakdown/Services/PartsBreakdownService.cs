// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartsBreakdownService.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the PartsBreakdownService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Aveva.Engineering.PartsBreakdown.Models;
using Aveva.Engineering.PartsBreakdown.Repositories;

namespace Aveva.Engineering.PartsBreakdown.Services
{
    /// <summary>
    /// Class PartsBreakdownService.
    /// </summary>
    public class PartsBreakdownService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IPartsBreakdownRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartsBreakdownService"/> class.
        /// </summary>
        /// <param name="partsBreakdownRepository">The parts breakdown repository.</param>
        public PartsBreakdownService(IPartsBreakdownRepository partsBreakdownRepository)
        {
            repository = partsBreakdownRepository;
        }

        /// <summary>
        /// Gets the part associations.
        /// </summary>
        /// <returns>Collection of Associations.</returns>
        public IEnumerable<Association> GetPartAssociations()
        {
            return repository.GetPartAssociations();
        }

        /// <summary>
        /// Gets the parts break down.
        /// </summary>
        /// <param name="associations">The associations.</param>
        /// <returns>Collection of  PartTypes.</returns>
        public IEnumerable<PartType> GetPartsBreakDown(IEnumerable<Association> associations)
        {
            // Assumption is that UI has already checked if anything is selected or not e.g. Selection.CurrentSelection != null && Selection.CurrentSelection.Members.Any()
            var retVal = new List<PartType>();

            foreach (Association association in associations)
            {
                IEnumerable<PartType> defaultPartTypes = repository.GetRangeClassPartTypes(association);
                IEnumerable<TagClassCount> TagClassCounts = repository.GetTagClassCounts(association);

                // Collect Sub-Class URIs that are already being referenced by selected items
                var distinctClassesOfExistingParts = TagClassCounts.SelectMany(tpc => tpc.ClassCountDictionary.Keys)
                    .Distinct().ToList();
                var distinctClassUrisOfExistingParts = distinctClassesOfExistingParts.Select(uri => uri).ToList();
                var defaultPartTypeClassUris = defaultPartTypes.Select(pt => pt.BaseClassUri).ToList();
                var subClassUris = distinctClassUrisOfExistingParts.Except(defaultPartTypeClassUris).ToList();

                // Get part types for existing parts that are sub classes of classes in Associations range
                IEnumerable<PartType> existingSubClassPartTypes =
                    repository.GetSubClassPartTypesFromExistingParts(association, subClassUris);
                var defaultAndExistingPartTypes = defaultPartTypes.Concat(existingSubClassPartTypes);

                foreach (var partType in defaultAndExistingPartTypes)
                {
                    foreach (var tagClassCount in TagClassCounts)
                    {
                        var partTagQuantity = tagClassCount.GetPartTagQuantity(partType.ActualClassUri);
                        if (partTagQuantity != null)
                        {
                            partType.AddPartTagQuantity(partTagQuantity);
                        }
                    }
                }

                retVal.AddRange(defaultAndExistingPartTypes);
            }

            return retVal;
        }

        /// <summary>
        /// Determines whether [is selection valid].
        /// </summary>
        /// <returns><c>true</c> if [is selection valid]; otherwise, <c>false</c>.</returns>
        public bool IsSelectionValid()
        {
            if (repository.GetSelectionClassCount() == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Filters out the class uris that don't have child classes.
        /// </summary>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns>List of classes that have child classes.</returns>
        internal IEnumerable<string> FilterClassUrisWithChildren(List<string> allRangeClassUris)
        {
            return repository.FilterClassUrisWithChildren(allRangeClassUris);
        }

        /// <summary>
        /// Gets the part type for class identifier.
        /// </summary>
        /// <param name="subClassUri">The sub class identifier.</param>
        /// <param name="associations">The associations.</param>
        /// <returns>PartType for subClassUri</returns>
        internal PartType GetPartTypeForSubClassUri(string subClassUri, IEnumerable<Association> associations)
        {
            PartType retVal = null;
            var allRangeClasses = associations.SelectMany(p => p.RangeClasses);
            var allRangeClassUris = allRangeClasses.Select(p => p.ClassUri).ToList();
            string subClassName;
            string baseClassUri;
            string baseClassName;
            if (repository.GetSubClassInfo(subClassUri, out subClassName, out baseClassUri, out baseClassName, allRangeClassUris))
            {
                var association = associations.FirstOrDefault(assoc => assoc.RangeClasses.Select(cls => cls.ClassUri).Contains(baseClassUri));
                retVal = new PartType(baseClassName, baseClassUri, subClassName, subClassUri, association);
            }

            return retVal;
        }
    }
}