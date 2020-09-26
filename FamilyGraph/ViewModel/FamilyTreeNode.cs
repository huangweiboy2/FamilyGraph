using System.Collections.ObjectModel;
using FamilyGraph.Internal;
using GalaSoft.MvvmLight;

namespace FamilyGraph.ViewModel
{
    public class FamilyTreeNode : ViewModelBase
    {
        public FamilyTreeNode ParentNode;
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

        private Gender _type = Gender.Male;

        public Gender Type
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

        public void AddChild(FamilyTreeNode node)
        {
            node.ParentNode = this;
            Children.Add(node);
        }
    }
}