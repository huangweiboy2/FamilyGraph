using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FamilyGraph.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        private FamilyTreeNode _node;
        public FamilyTreeNode Node
        {
            get => _node;
            set
            {
                _node = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand Submit { get; set; }
        public RelayCommand Cancel { get; set; }
    }
}