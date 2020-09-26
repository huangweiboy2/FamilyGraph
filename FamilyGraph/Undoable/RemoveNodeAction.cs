using FamilyGraph.ViewModel;

namespace FamilyGraph.Undoable
{
    public class RemoveNodeAction:IUndoAction
    {

        private FamilyTreeNode _node;

        public RemoveNodeAction(FamilyTreeNode node)
        {
            _node = node;
        }

        public void Do()
        {
            _node.ParentNode?.Children.Remove(_node);
        }

        public void Undo()
        {
            _node.ParentNode?.AddChild(_node);
        }
    }
}