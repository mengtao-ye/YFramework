using System;
using UnityEngine;
using YFramework;

namespace YFramework
{
    public interface ITransformToTween  : IPool,ILife ,IToTween
    {
        Transform transform { get; }
    }
}
