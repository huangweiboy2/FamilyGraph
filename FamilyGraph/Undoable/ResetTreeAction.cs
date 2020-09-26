using System.Collections.ObjectModel;
using FamilyGraph.Internal;
using FamilyGraph.ViewModel;

namespace FamilyGraph.Undoable
{
    public class ResetTreeAction : IUndoAction
    {
        private MainViewModel _viewModel;
        private ObservableCollection<FamilyTreeNode> _oldNodes;
        private ObservableCollection<FamilyTreeNode> _emptyNodes;

        public ResetTreeAction(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            _oldNodes = _viewModel.FamilyTreeNodes;
            _emptyNodes = new ObservableCollection<FamilyTreeNode>
            {
                new FamilyTreeNode
                {
                    Name = "祖宗",
                    Type = Gender.Male
                }
            };
        }

        public void Do()
        {
            _viewModel.FamilyTreeNodes = _emptyNodes;
        }

        public void Undo()
        {
            _viewModel.FamilyTreeNodes = _oldNodes;
        }
    }
}