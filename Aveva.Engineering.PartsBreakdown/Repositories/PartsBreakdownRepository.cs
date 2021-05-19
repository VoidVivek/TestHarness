// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartsBreakdownRepository.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the PartsBreakdownRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Aveva.Core.Database;
using Aveva.Core.Presentation;
using Aveva.DataModelling.ApplicationInterface;
using Aveva.Engineering.ClassModelManager;
using Aveva.Engineering.PartsBreakdown.Models;

namespace Aveva.Engineering.PartsBreakdown.Repositories
{
    /// <summary>
    /// Class PartsBreakdownRepository.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.Repositories.IPartsBreakdownRepository" />
    internal class PartsBreakdownRepository : IPartsBreakdownRepository
    {
        /// <summary>
        /// Gets the part associations.
        /// </summary>
        /// <returns>Collection of Associations.</returns>
        public IEnumerable<Association> GetPartAssociations()
        {
            var retVal = new List<Association>();
            if (Selection.CurrentSelection != null && Selection.CurrentSelection.Members.Any())
            {
                var partAssociations =
                    ModelProvider.GetPartAssociationsForDbElementType(
                        Selection.CurrentSelection.Members[0].GetActualType());
                foreach (var partAssociation in partAssociations)
                {
                    var implementations = partAssociation.Implementations.Select(imp => imp.Name);
                    var rangeClasses = partAssociation.RangeClasses.Select(rc => new Class(rc.URI, rc.Name));
                    var association = new Association(
                        partAssociation.URI,
                        partAssociation.Name,
                        implementations,
                        rangeClasses,
                        partAssociation.Cardinality);
                    retVal.Add(association);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the range class part types.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  PartTypes.</returns>
        public IEnumerable<PartType> GetRangeClassPartTypes(Association association)
        {
            var retVal = new List<PartType>();

            foreach (var rangeClass in association.RangeClasses)
            {
                var partType = new PartType(
                    rangeClass.ClassName,
                    rangeClass.ClassUri,
                    rangeClass.ClassName,
                    rangeClass.ClassUri,
                    association);

                retVal.Add(partType);
            }

            return retVal;
        }

        /// <summary>
        /// Gets the selection class count.
        /// </summary>
        /// <returns>Integer value</returns>
        public int GetSelectionClassCount()
        {
            int retVal = 0;
            if (Selection.CurrentSelection != null && Selection.CurrentSelection.Members.Any())
            {
                List<IModelClass> modelClasses = new List<IModelClass>();

                foreach (var selectedDbElement in Selection.CurrentSelection.Members)
                {
                    var modelClass = ModelProvider.GetClassForDbElementType(selectedDbElement.GetActualType());

                    if (!modelClasses.Contains(modelClass))
                    {
                        modelClasses.Add(modelClass);
                    }
                }

                retVal = modelClasses.Count;
                modelClasses.Clear();
            }

            return retVal;
        }

        /// <summary>
        /// Gets the sub class part types from existing parts.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="subClassUris">The sub class uris.</param>
        /// <returns>Collection of  PartTypes.</returns>
        public IEnumerable<PartType> GetSubClassPartTypesFromExistingParts(
            Association association,
            List<string> subClassUris)
        {
            var retVal = new List<PartType>();

            foreach (var subClassUri in subClassUris)
            {
                string subClassName;
                if (ModelProvider.GetClassNameForUri(subClassUri, out subClassName))
                {
                    var partType = new PartType(string.Empty, string.Empty, subClassName, subClassUri, association);

                    retVal.Add(partType);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the tag class counts.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  TagClassCounts.</returns>
        public IEnumerable<TagClassCount> GetTagClassCounts(Association association)
        {
            var retVal = new List<TagClassCount>();
            var dbAttributes = association.AttributeNames.Select(DbAttribute.GetDbAttribute).Where(att => att.IsValid);

            foreach (var selectedDbElement in Selection.CurrentSelection.Members)
            {
                var tagClassCount = new TagClassCount(selectedDbElement, dbAttributes);

                retVal.Add(tagClassCount);
            }

            return retVal;
        }

        /// <summary>
        /// Filters out the class uris that don't have child classes.
        /// </summary>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns>List of classes that have child classes.</returns>
        public IEnumerable<string> FilterClassUrisWithChildren(List<string> allRangeClassUris)
        {
            var retVal = new List<string>();
            foreach (var rangeClassUri in allRangeClassUris)
            {
                if (ModelProvider.ClassHasChildren(rangeClassUri))
                {
                    retVal.Add(rangeClassUri);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the sub class information.
        /// </summary>
        /// <param name="subClassUri">The sub class URI.</param>
        /// <param name="subClassName">Name of the sub class.</param>
        /// <param name="baseClassUri">The base class URI.</param>
        /// <param name="baseClassName">Name of the base class.</param>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns><c>true</c> if base class for sub class is found from allRangeClassUris, <c>false</c> otherwise.</returns>
        public bool GetSubClassInfo(
            string subClassUri,
            out string subClassName,
            out string baseClassUri,
            out string baseClassName,
            List<string> allRangeClassUris)
        {
            return ModelProvider.GetSubClassInfo(
                subClassUri,
                out subClassName,
                out baseClassUri,
                out baseClassName,
                allRangeClassUris);
        }
    }
}