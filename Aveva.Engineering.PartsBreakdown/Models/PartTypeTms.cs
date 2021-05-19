// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartTypeTms.cs" company="AVEVA Solutions Limited">
//   Copyright 2009 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved.
// </copyright>
// <summary>
//   Defines the PartTypeTms type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Aveva.Engineering.TMS.Addin.ViewModel;

namespace Aveva.Engineering.PartsBreakdown.Models
{
    /// <summary>
    /// Class PartTypeTms, extension of PartType to handle TMS naming rules
    /// </summary>
    /// <seealso cref="Aveva.Engineering.PartsBreakdown.Models.PartType" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PartTypeTms : PartType, INotifyPropertyChanged
    {
        /// <summary>
        /// The naming view models to allow data entry of naming rule parts
        /// </summary>
        private ObservableCollection<RequestFormViewModel> namingViewModels;

        /// <summary>
        /// The request form view model creator
        /// </summary>
        private IRequestFormViewModelCreator requestFormViewModelCreator;

        public PartTypeTms(
            string baseClassName,
            string baseClassUri,
            string actualClassName,
            string actualClassUri,
            Association association)
            : base(baseClassName, baseClassUri, actualClassName, actualClassUri, association)
        {
            namingViewModels = new ObservableCollection<RequestFormViewModel>();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the naming view models.
        /// </summary>
        /// <value>The naming view models.</value>
        public ObservableCollection<RequestFormViewModel> NamingViewModels
        {
            get
            {
                return namingViewModels;
            }

            set
            {
                namingViewModels = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Visibility ListBoxVisibility { get; set; }
 
        /// <summary>
        /// Gets or sets the requested quantity.
        /// </summary>
        /// <value>The requested quantity.</value>
        public override int RequestedQuantity
        {
            get
            {
                return requestedQuantity;
            }

            set
            {
                if (this.requestedQuantity == value)
                {
                    ListBoxVisibility = System.Windows.Visibility.Visible;
                    return;
                }

                if (value == 0)
                {
                    ListBoxVisibility = System.Windows.Visibility.Collapsed;
                    namingViewModels.Clear();
                }
                else if (requestedQuantity > value)
                {
                    ListBoxVisibility = System.Windows.Visibility.Visible;
                    for (int i = namingViewModels.Count - 1; i >= value; i--)
                    {
                        namingViewModels.RemoveAt(i);
                    }
                }
                else if (requestedQuantity < value)
                {
                    ListBoxVisibility = System.Windows.Visibility.Visible;
                    while (namingViewModels.Count < value)
                    {
                        int index = namingViewModels.Count+1;
                        this.namingViewModels.Add(requestFormViewModelCreator.Create(ActualClassUri, ActualClassName+" "+index.ToString()));
                    }
                }

                requestedQuantity = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Sets the naming view model creator.
        /// </summary>
        /// <param name="requestFormViewModelCreatorInstance">The instance to be set.</param>
        internal void SetNamingViewModelCreator(IRequestFormViewModelCreator requestFormViewModelCreatorInstance)
        {
            this.requestFormViewModelCreator = requestFormViewModelCreatorInstance;
        }

        /// <summary>
        /// Transfers the part tag quantities from base part type instance.
        /// </summary>
        /// <param name="partType">Type of the part.</param>
        internal void TransferPartTagQuantities(PartType partType)
        {
            partTagQuantities = partType.PartTagQuantities.ToList();
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}