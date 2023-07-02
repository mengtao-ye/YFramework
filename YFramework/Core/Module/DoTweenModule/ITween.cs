using System;
using UnityEngine;

namespace YFramework
{
    public interface ITween
    {
        void OnComplete(Action complete);
        Transform transform { get; }
        float time { get; }
        void Complete();
    }
}
