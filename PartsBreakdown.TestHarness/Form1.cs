using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

using Aveva.Core.Engineering.Interface;
using Aveva.Engineering.PartsBreakdown.ViewModels;
using Aveva.Engineering.PartsBreakdown.Views;

using MessageBox = System.Windows.Forms.MessageBox;

namespace PartsBreakdown.TestHarness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var fakeAccessClassPicker = new FakeAccessClassPicker();
            Helper.Resolver().DependencyContainer.Register<IAccessClassPicker>(fakeAccessClassPicker);
        }

        public void SetOwner(Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            helper.Owner = this.Handle;
        }

        private void btnNonTmsEnabled_Click(object sender, EventArgs e)
        {
            this.ShowPartsBreakDown(new FakeMainWindowViewModelFactory().Create());
        }

        private void btnTmsEnabled_Click(object sender, EventArgs e)
        {
            this.ShowPartsBreakDown(new FakeTmsMainWindowViewModelFactory().Create());
        }

        private void ShowPartsBreakDown(MainWindowViewModel viewModel)
        {
            if (viewModel.IsSelectionValid())
            {
                MainWindow win =
                    new MainWindow(viewModel) { WindowStartupLocation = WindowStartupLocation.CenterOwner };
                viewModel.LoadData();
                this.SetOwner(win);
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selected tags don't have same class", "Error");
            }
        }
    }
}