using UnityEngine;

namespace YFramework
{
    public interface IModel : ILife
    {
        Transform transform { get; }
        T GetChildModel<T>() where T : class, IChildModel;
    }
}
