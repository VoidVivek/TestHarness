// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagAndClassCount.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the TagClassCount type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Aveva.Core.Database;
using Aveva.Engineering.Info;

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class to hold TagNo and Class Count of its parts.
    /// </summary>
    public class TagClassCount
    {
        /// <summary>
        /// The class count dictionary
        /// </summary>
        private readonly Dictionary<string, int> classCountDictionary = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TagClassCount"/> class.
        /// </summary>
        /// <param name="dbElement">The database element.</param>
        /// <param name="dbAttributes">The database attributes.</param>
        public TagClassCount(DbElement dbElement, IEnumerable<DbAttribute> dbAttributes)
        {
            if (dbElement.IsValidEx())  // && !dbElement.GetBool(DbAttributeInstance.EXCLEL))
            {
                DbAttribute dbAttributeDisplayName =
                    EngineeringInfo.CurrentDisplayNameAttribute(dbElement.GetActualType());
                if (dbAttributeDisplayName == null)
                {
                    dbAttributeDisplayName = DbAttributeInstance.NAMN;
                }

                TagNo = dbElement.GetString(dbAttributeDisplayName);

                foreach (DbAttribute dbAttribute in dbAttributes)
                {
                    if (dbAttribute.MaximumSize == 1)
                    {
                        DbElement dbElementPart = dbElement.GetElement(dbAttribute);
                        ProcessPart(dbElementPart);
                    }
                    else
                    {
                        DbElement[] dbElementPartArray = dbElement.GetElementArray(dbAttribute);
                        foreach (DbElement dbElementPart in dbElementPartArray)
                        {
                            ProcessPart(dbElementPart);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TagClassCount"/> class from being created.
        /// </summary>
        private TagClassCount()
        {
        }

        /// <summary>
        /// Gets the class count dictionary.
        /// </summary>
        /// <value>The class count dictionary.</value>
        public IDictionary<string, int> ClassCountDictionary
        {
            get
            {
                return this.classCountDictionary;
            }
        }

        /// <summary>
        /// Gets or sets the tag no.
        /// </summary>
        /// <value>The tag no.</value>
        public string TagNo { get; set; }

        /// <summary>
        /// Gets the part tag quantity.
        /// </summary>
        /// <param name="classUri">The class URI.</param>
        /// <returns>Part's TagQuantity.</returns>
        public PartTagQuantity GetPartTagQuantity(string classUri)
        {
            PartTagQuantity retVal = null;

            var modelClass = this.ClassCountDictionary.Keys.FirstOrDefault(uri => uri == classUri);
            if (modelClass != null)
            {
                retVal = new PartTagQuantity(TagNo, this.ClassCountDictionary[modelClass]);
            }

            return retVal;
        }

        /// <summary>
        /// Processes the part and adds it to Class Count dictionary.
        /// </summary>
        /// <param name="dbElementPart">The database element part.</param>
        private void ProcessPart(DbElement dbElementPart)
        {
            if (dbElementPart != null)
            {
                var modelClass = ModelProvider.GetClassForDbElementType(dbElementPart.GetActualType());
                if (modelClass != null)
                {
                    if (!this.ClassCountDictionary.ContainsKey(modelClass.URI))
                    {
                        this.ClassCountDictionary.Add(modelClass.URI, 0);
                    }

                    this.ClassCountDictionary[modelClass.URI]++;
                }
            }
        }
    }
}