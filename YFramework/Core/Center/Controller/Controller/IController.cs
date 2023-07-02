namespace YFramework
{
    public interface IController : ILife
    {
        T GetChildController<T>() where T : class, IChildController;
    }
}
