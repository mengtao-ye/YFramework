namespace YFramework
{
    public interface IUnDo
    {
        string name { get; }
        void Undo();
        void Do();
        void Redo();
    }
}
