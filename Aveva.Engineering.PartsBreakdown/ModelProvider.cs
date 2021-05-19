// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProvider.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Class ModelProvider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Aveva.ApplicationFramework;
using Aveva.Core.Database;
using Aveva.DataModelling.ApplicationInterface;
using Aveva.Engineering.ClassModelManager;

namespace Aveva.Engineering.PartsBreakdown
{
    /// <summary>
    /// Class ModelProvider.
    /// </summary>
    internal class ModelProvider
    {
        /// <summary>
        /// The can have part
        /// </summary>
        public const string CanHavePart = "http://www.aveva.com/vocabulary/basevocabulary#canHavePart";

        /// <summary>
        /// The has part
        /// </summary>
        public const string HasPart = "http://www.aveva.com/vocabulary/aiae#HasPart";

        /// <summary>
        /// Classes the has children.
        /// </summary>
        /// <param name="classUri">The class URI.</param>
        /// <returns><c>true</c> if class has child classes, <c>false</c> otherwise.</returns>
        public static bool ClassHasChildren(string classUri)
        {
            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            var modelClass = dataModelProvider.GetClassByURI(classUri);

            return (modelClass != null) && (modelClass.Children != null) && modelClass.Children.Any();
        }

        /// <summary>
        /// Gets the type of the class for database element.
        /// </summary>
        /// <param name="dbElementType">Type of the database element.</param>
        /// <returns>Model Class.</returns>
        public static IModelClass GetClassForDbElementType(DbElementType dbElementType)
        {
            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            return dataModelProvider.GetClassForDbElementType(dbElementType);
        }

        // Todo: rEMOVE THIS Method

        /// <summary>
        /// Gets the type of the class from typical database element.
        /// REMOVE THIS METHOD and REPLACE calls with GetClassForDbElementType
        /// </summary>
        /// <param name="dbElementType">Type of the database element.</param>
        /// <returns> Data Model Class </returns>
        public static IModelClass GetClassFromTypicalDbElementType(DbElementType dbElementType)
        {
            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            var modelClass = dataModelProvider.GetClassForDbElementType(dbElementType);
            return dataModelProvider.GetClassForDbElementType(modelClass.Implementation);
        }

        /// <summary>
        /// Gets the class name for URI.
        /// </summary>
        /// <param name="classUri">The class URI.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns><c>true</c> if class found, <c>false</c> otherwise.</returns>
        public static bool GetClassNameForUri(string classUri, out string className)
        {
            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            var modelClass = dataModelProvider.GetClassByURI(classUri);

            if (modelClass == null)
            {
                className = string.Empty;
                return false;
            }

            className = modelClass.Name;
            return true;
        }

        /// <summary>
        /// Gets the type of the part associations for database element.
        /// </summary>
        /// <param name="dbElementType">Type of the database element.</param>
        /// <returns>List of IModelClassAssociation.</returns>
        public static IEnumerable<IModelClassAssociation> GetPartAssociationsForDbElementType(
            DbElementType dbElementType)
        {
            var modelClass = GetClassFromTypicalDbElementType(dbElementType);
            var hasPartAssociations = GetClassAssociationsForBaseUri(modelClass, HasPart);
            var canHavePartAssociations = GetClassAssociationsForBaseUri(modelClass, CanHavePart);
            return hasPartAssociations.Concat(canHavePartAssociations);
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
        public static bool GetSubClassInfo(
            string subClassUri,
            out string subClassName,
            out string baseClassUri,
            out string baseClassName,
            List<string> allRangeClassUris)
        {
            subClassName = string.Empty;
            baseClassUri = string.Empty;
            baseClassName = string.Empty;

            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();

            var parentKeys = dataModelProvider.GetParentKeys(subClassUri);
            var foundParentKey = CheckParents(allRangeClassUris, parentKeys);
            if (foundParentKey != string.Empty)
            {
                var subClass = dataModelProvider.GetClassByURI(subClassUri);
                var baseClass = dataModelProvider.GetClassByURI(foundParentKey);

                subClassName = subClass.Name;
                baseClassName = baseClass.Name;
                baseClassUri = foundParentKey;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the parents.
        /// </summary>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <param name="parentKeys">The parent keys.</param>
        /// <returns>Uri of found parent</returns>
        private static string CheckParents(List<string> allRangeClassUris, IEnumerable<string> parentKeys)
        {
            foreach (var parentKey in parentKeys)
            {
                if (allRangeClassUris.Contains(parentKey))
                {
                    return parentKey;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the base associations uris.
        /// </summary>
        /// <param name="baseAssociationUri">The base association URI.</param>
        /// <returns>List of URIs for base association.</returns>
        private static IEnumerable<string> GetBaseAssociationsUris(string baseAssociationUri)
        {
            var retVal = new List<string>();
            var dataModelProvider = DependencyResolver.Resolver.GetImplementationOf<IDataModelProvider>();
            var associations = dataModelProvider.GetAssociationsOfSubProperty(baseAssociationUri);

            if (associations != null)
            {
                retVal = associations.Select(assoc => assoc.URI).ToList();
            }

            return retVal;
        }

        /// <summary>
        /// Gets the class associations for base URI.
        /// </summary>
        /// <param name="modelClass">The model class.</param>
        /// <param name="baseAssociationUri">The base association URI.</param>
        /// <returns>List of IModelClassAssociation.</returns>
        private static IEnumerable<IModelClassAssociation> GetClassAssociationsForBaseUri(
            IModelClass modelClass,
            string baseAssociationUri)
        {
            var retVal = new List<IModelClassAssociation>();
            var baseAssociations = GetBaseAssociationsUris(baseAssociationUri);

            foreach (var baseAssociation in baseAssociations)
            {
                var baseSubAssociations = modelClass.Associations.Where(x => x.AssociationType.URI == baseAssociation);

                foreach (var baseSubAssociation in baseSubAssociations)
                {
                    if (baseSubAssociation.Implementations != null && baseSubAssociation.Implementations.Any())
                    {
                        retVal.Add(baseSubAssociation);
                    }
                }
            }

            return retVal;
        }
    }
}