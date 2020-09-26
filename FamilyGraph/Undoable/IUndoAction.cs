namespace FamilyGraph.Undoable
{
    public interface IUndoAction
    {
        void Do();
        void Undo();
    }
}