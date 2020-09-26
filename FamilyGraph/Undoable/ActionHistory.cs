using System.Collections.Generic;

namespace FamilyGraph.Undoable
{
    public class ActionHistory
    {

        private readonly Stack<IUndoAction> _doneActions = new Stack<IUndoAction>();
        private readonly Stack<IUndoAction> _canRedoActions = new Stack<IUndoAction>();

        public int UndoCount => _doneActions.Count;
        public int RedoCount => _canRedoActions.Count;
        
        public void DoAction(IUndoAction action)
        {
            action.Do();
            _doneActions.Push(action);
        }

        public void Undo(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var action = _doneActions.Pop();
                action.Undo();
                _canRedoActions.Push(action);
            }
        }

        public void Redo(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var action = _canRedoActions.Pop();
                if (action == null)
                    return;
                DoAction(action);
            }
        }

        public void Clear()
        {
            _doneActions.Clear();
            _canRedoActions.Clear();
        }
    }
}