namespace YFramework
{
    public interface IModule : ILife
    {
        bool isRun { get; set; }
        Center center { get; }
    }
}
