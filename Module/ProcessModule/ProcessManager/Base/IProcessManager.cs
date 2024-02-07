namespace YFramework
{
    public interface IProcessManager
    {
        void DoNext();
        void Update();
        void SetProcessManager(ProcessController processController);
        IProcess Concat(IProcess process);
        void Launcher();
    }
}
