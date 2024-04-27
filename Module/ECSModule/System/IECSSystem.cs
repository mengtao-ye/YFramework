namespace YFramework
{
    public interface IECSSystem :ILife
    {
        void RemoveEntity(IECSEntity entity);
        IECSEntity FindEntity(long id);
        void AddEntity(IECSEntity entity);
    }
}
