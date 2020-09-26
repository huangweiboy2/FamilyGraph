using System.Collections.ObjectModel;
using FamilyGraph.ViewModel;

namespace FamilyGraph.Undoable
{
    public class AddChildAction : IUndoAction
    {
        private FamilyTreeNode _node;
        private FamilyTreeNode _subNode;

        public AddChildAction(FamilyTreeNode node, FamilyTreeNode subNode)
        {
            _node = node;
            _subNode = subNode;
        }

        public void Do()
        {
            _node.AddChild(_subNode);
        }

        public void Undo()
        {
            _node.Children.Remove(_subNode);
        }
    }
}