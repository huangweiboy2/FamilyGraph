using System.Collections.Generic;
using System.Collections.ObjectModel;
using FamilyGraph.Internal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FamilyGraph.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<FamilyTreeNode> _familyTreeNodes = new ObservableCollection<FamilyTreeNode>();

        public ObservableCollection<FamilyTreeNode> FamilyTreeNodes
        {
            get => _familyTreeNodes;
            set
            {
                _familyTreeNodes = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<FamilyTreeNode> AddEmptyChild { get; set; }
        public RelayCommand<FamilyTreeNode> RemoveCurrentNode { get; set; }

        public MainViewModel()
        {
            AddEmptyChild = new RelayCommand<FamilyTreeNode>(node => node.Children.Add(new FamilyTreeNode
            {
                Name = "你说取个啥名好呢？",
                Type = Gender.Male
            }));
            RemoveCurrentNode = new RelayCommand<FamilyTreeNode>(node => node.ParentNode?.Children.Remove(node));
        }
    }
}