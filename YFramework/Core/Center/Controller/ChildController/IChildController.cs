namespace YFramework
{
    public interface IChildController : ILife
    {
        IController controller { get; }
    }
}
