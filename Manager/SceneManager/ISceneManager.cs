using System;

namespace YFramework
{
   public  interface ISceneManager
    {
        IMap<string, IScene> sceneMap { get; }
        bool loading { get; }
        IScene curScene { get; }
        IScene preScene { get; }

        void LoadScene(string sceneName, Action<float> loadAction = null);
        void SetStartLoadCallBack(Action<string> startLoad);
        void SetLoadCompleteCallBack(Action<string> loadSceneComplete);
    }
}
