namespace YFramework
{
    public interface IScene :ILife
    {
        string sceneName { get; }
        ISceneManager sceneManager { get; }
        ICanvas canvas { get; }
        IModel model { get; }
        IController controller { get; }
        void SetSceneManager(ISceneManager manager);
    }
}
