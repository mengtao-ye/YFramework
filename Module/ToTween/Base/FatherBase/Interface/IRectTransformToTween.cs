using System;
using UnityEngine;
using YFramework;

namespace YFramework
{
    public interface IRectTransformToTween : IPool,ILife, IToTween
    {
        RectTransform rectTransform { get; }
    }
}
