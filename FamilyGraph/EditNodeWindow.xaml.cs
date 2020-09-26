using System.Windows;
using FamilyGraph.ViewModel;
using GalaSoft.MvvmLight.Command;

namespace FamilyGraph
{
    public partial class EditNodeWindow : Window
    {
        private EditViewModel _viewModel;

        public FamilyTreeNode Node => _viewModel?.Node;

        public EditNodeWindow(FamilyTreeNode node)
        {
            InitializeComponent();
            InitViewModel();
            _viewModel.Node = new FamilyTreeNode
            {
                Name = node.Name,
                SpouseName = node.SpouseName,
                Type = node.Type
            };
        }

        private void InitViewModel()
        {
            _viewModel = new EditViewModel
            {
                Submit = new RelayCommand(() =>
                {
                    DialogResult = true;
                    Close();
                }),
                Cancel = new RelayCommand(() =>
                {
                    DialogResult = false;
                    Close();
                })
            };
            DataContext = _viewModel;
        }
    }
}