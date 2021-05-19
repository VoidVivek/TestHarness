// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakePartsBreakDownRepository.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   The Fake parts break down repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

using Aveva.Engineering.PartsBreakdown.Models;
using Aveva.Engineering.PartsBreakdown.Repositories;

namespace PartsBreakdown.TestHarness
{
    /// <summary>
    /// Class FakePartsBreakDownRepository.
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.Repositories.IPartsBreakdownRepository" />
    internal class FakePartsBreakDownRepository : IPartsBreakdownRepository
    {
        /// <summary>
        /// Gets the part associations.
        /// </summary>
        /// <returns>Collection of Associations.</returns>
        public IEnumerable<Association> GetPartAssociations()
        {
            var retVal = DeSerialise<List<Association>>("Associations.xml");
            return retVal;
        }

        /// <summary>
        /// Gets the range class part types.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  PartTypes.</returns>
        public IEnumerable<PartType> GetRangeClassPartTypes(Association association)
        {
            string associationUri = association.AssociationUri;

            switch (associationUri)
            {
                case "http://www.aveva.com/datamodel/item#ba6740eed-ddd6-45fa-9809-bccc0fffb0e2":
                    return DeSerialise<List<PartType>>("PartType_1.xml");
                case "http://www.aveva.com/datamodel/item#b934e790c-4c38-443e-9ac0-12c38b3cd043":
                    return DeSerialise<List<PartType>>("PartType_2.xml");
                case "http://www.aveva.com/datamodel/item#b71294789-9730-4932-bca6-240d4847762b":
                    return DeSerialise<List<PartType>>("PartType_3.xml");
                case "http://www.aveva.com/datamodel/item#b2f674ab1-79b8-4280-8c62-165a5de92997":
                    return DeSerialise<List<PartType>>("PartType_4.xml");
                case "http://www.aveva.com/datamodel/item#b676e556d-e6d5-41b6-b419-3e28166185e6":
                    return DeSerialise<List<PartType>>("PartType_5.xml");
                default:
                    return new List<PartType>();
            }
        }

        /// <summary>
        /// Gets the selection class count.
        /// </summary>
        /// <returns>Integer value</returns>
        public int GetSelectionClassCount()
        {
            return 1;
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
            string associationUri = association.AssociationUri;

            switch (associationUri)
            {
                case "http://www.aveva.com/datamodel/item#ba6740eed-ddd6-45fa-9809-bccc0fffb0e2":
                    return DeSerialise<List<PartType>>("PartType_1.1.xml");
                case "http://www.aveva.com/datamodel/item#b934e790c-4c38-443e-9ac0-12c38b3cd043":
                    return DeSerialise<List<PartType>>("PartType_2.1.xml");
                case "http://www.aveva.com/datamodel/item#b71294789-9730-4932-bca6-240d4847762b":
                    return DeSerialise<List<PartType>>("PartType_3.1.xml");
                case "http://www.aveva.com/datamodel/item#b2f674ab1-79b8-4280-8c62-165a5de92997":
                    return DeSerialise<List<PartType>>("PartType_4.1.xml");
                case "http://www.aveva.com/datamodel/item#b676e556d-e6d5-41b6-b419-3e28166185e6":
                    return DeSerialise<List<PartType>>("PartType_5.1.xml");
                default:
                    return new List<PartType>();
            }
        }

        /// <summary>
        /// Gets the tag class counts.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>Collection of  TagClassCounts.</returns>
        public IEnumerable<TagClassCount> GetTagClassCounts(Association association)
        {
            string associationUri = association.AssociationUri;

            switch (associationUri)
            {
                case "http://www.aveva.com/datamodel/item#ba6740eed-ddd6-45fa-9809-bccc0fffb0e2":
                    return DeSerialise<List<TagClassCount>>("TagClassCount_1.xml");
                case "http://www.aveva.com/datamodel/item#b934e790c-4c38-443e-9ac0-12c38b3cd043":
                    return DeSerialise<List<TagClassCount>>("TagClassCount_2.xml");
                case "http://www.aveva.com/datamodel/item#b71294789-9730-4932-bca6-240d4847762b":
                    return DeSerialise<List<TagClassCount>>("TagClassCount_3.xml");
                case "http://www.aveva.com/datamodel/item#b2f674ab1-79b8-4280-8c62-165a5de92997":
                    return DeSerialise<List<TagClassCount>>("TagClassCount_4.xml");
                case "http://www.aveva.com/datamodel/item#b676e556d-e6d5-41b6-b419-3e28166185e6":
                    return DeSerialise<List<TagClassCount>>("TagClassCount_5.xml");
                default:
                    return new List<TagClassCount>();
            }
        }

        /// <summary>
        /// Filters out the class uris that don't have child classes.
        /// </summary>
        /// <param name="allRangeClassUris">All range class uris.</param>
        /// <returns>List of classes that have child classes.</returns>
        public IEnumerable<string> FilterClassUrisWithChildren(List<string> allRangeClassUris)
        {
            // return half of the passed
            int half = allRangeClassUris.Count / 2;
            return allRangeClassUris.Take(half);
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
            subClassName = "Fake Sub Wire";

            // Uri of Wire
            baseClassUri = "http://www.aveva.com/datamodel/item#bce01f86d-5d9a-450c-a35b-9dc951c7d68e";
            baseClassName = "Wire";
            return true;
        }

        /// <summary>
        /// De-Serializes objects from files.
        /// </summary>
        /// <typeparam name="T">Type of</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Type of the de-serialized</returns>
        private T DeSerialise<T>(string fileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        using (Stream streamMem = new MemoryStream())
                        {
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(result);
                            streamMem.Write(data, 0, data.Length);
                            streamMem.Position = 0;
                            DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                            return (T)deserializer.ReadObject(streamMem);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default(T);
        }
    }
}