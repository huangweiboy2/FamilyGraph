using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using GalaSoft.MvvmLight;

namespace FamilyGraph
{
    public class FamilyTreeNode : ViewModelBase
    {
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _spouseName = string.Empty;

        public string SpouseName
        {
            get => _spouseName;
            set
            {
                _spouseName = value;
                RaisePropertyChanged();
            }
        }

        private NodeType _type = NodeType.Male;

        public NodeType Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<FamilyTreeNode> _children = new ObservableCollection<FamilyTreeNode>();
        public ObservableCollection<FamilyTreeNode> Children
        {
            get => _children;
            set
            {
                _children = value;
                RaisePropertyChanged();
            }
        }

        public enum NodeType
        {
            Male,
            Female
        }
    }
}