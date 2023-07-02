using System.Collections.Generic;

namespace YFramework
{ 
    public class Center : ILife
    {
        private List<IModule> mGameList;
        public Center(Log log,Resource resource)
        {
            if (log == null) UnityEngine.Debug.LogError("请初始化Log");
            else Log.Instance = log;

            if (resource == null) UnityEngine.Debug.LogError("请初始化Resource");
            else Resource.Instance = resource;
            mGameList = new List<IModule>();
        }
       

        public void Awake()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].Awake();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].Clear();
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].FixedUpdate();
            }
        }

      

        public void LaterUpdate()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].LaterUpdate();
            }
        }

        public void OnDestory()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].OnDestory();
            }
        }

        public void Start()
        {

            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].Start();
            }
        }

        public void Update()
        {

            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].Update();
            }
        }


        public void AddGame(IModule game)
        {
            if (game == null) return;
            if (mGameList.Contains(game)) return;
            mGameList.Add(game);
        }

        public T GetGame<T>() where T : class,IModule
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                if (mGameList[i] is T) return mGameList[i] as T;
            }
            return default(T);
        }
    }
}