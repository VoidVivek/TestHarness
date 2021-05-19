// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the MainWindowViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

using Aveva.Engineering.PartsBreakdown.Commands;
using Aveva.Engineering.PartsBreakdown.Models;
using Aveva.Engineering.PartsBreakdown.Services;

namespace Aveva.Engineering.PartsBreakdown.ViewModels
{
    /// <summary>
    /// Class MainWindowViewModel.
    /// </summary>
    public class MainWindowViewModel
    {
        /// <summary>
        /// The associations
        /// </summary>
        private readonly IEnumerable<Association> associations;

        /// <summary>
        /// The service
        /// </summary>
        private readonly PartsBreakdownService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="partsBreakdownService">The parts breakdown service.</param>
        public MainWindowViewModel(PartsBreakdownService partsBreakdownService)
        {
            service = partsBreakdownService;
            associations = this.service.GetPartAssociations();

            AddPartTypeCommand = new AddPartTypeCommand(this);
            CreatePartsCommand = new CreatePartsCommand(this);
        }

        /// <summary>
        /// Gets the add part type command.
        /// </summary>
        /// <value>The add part type command.</value>
        public ICommand AddPartTypeCommand { get; private set; }

        /// <summary>
        /// Gets the create parts command.
        /// </summary>
        /// <value>The create parts command.</value>
        public ICommand CreatePartsCommand { get; private set; }

        /// <summary>
        /// Gets or sets the parts.
        /// </summary>
        /// <value>The parts.</value>
        public List<PartType> Parts { get; set; }

        /// <summary>
        /// Gets or sets the parts.
        /// </summary>
        /// <value>The parts.</value>
        public ICollectionView PartsCollectionView { get; set; }

        /// <summary>
        /// Adds the type of the part.
        /// </summary>
        /// <param name="classId">The class identifier.</param>
        /// <returns><c>true</c> if part type can be added, <c>false</c> otherwise.</returns>
        public bool AddPartType(string classId)
        {
            if (Parts.All(part => part.ActualClassUri != classId))
            {
                PartType partType = GetPartType(classId);

                if (partType != null)
                {
                    Parts.Add(partType);
                    PartsCollectionView.Refresh();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the base range classes.
        /// </summary>
        /// <returns>List of ClassUris.</returns>
        public List<string> GetBaseRangeClasses()
        {
            var allRangeClasses = associations.SelectMany(p => p.RangeClasses);
            var allRangeClassUris = allRangeClasses.Select(p => p.ClassUri).ToList();
            return service.FilterClassUrisWithChildren(allRangeClassUris).ToList();
        }

        /// <summary>
        /// Determines whether [is selection valid].
        /// </summary>
        /// <returns><c>true</c> if [is selection valid]; otherwise, <c>false</c>.</returns>
        public bool IsSelectionValid()
        {
            return service.IsSelectionValid();
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public virtual void LoadData()
        {
            Parts = service.GetPartsBreakDown(associations).ToList();
            SetCollectionView();
        }

        /// <summary>
        /// Gets part type for a sub class of association range class.
        /// </summary>
        /// <param name="classUri">The class identifier.</param>
        /// <returns>PartType for classUri</returns>
        protected virtual PartType GetPartType(string classUri)
        {
            return service.GetPartTypeForSubClassUri(classUri, associations);
        }

        /// <summary>
        /// Sets the collection view.
        /// </summary>
        protected virtual void SetCollectionView()
        {
            PartsCollectionView = CollectionViewSource.GetDefaultView(Parts);
            PartsCollectionView.SortDescriptions.Add(
                new SortDescription("ActualClassName", ListSortDirection.Ascending));
        }
    }
}