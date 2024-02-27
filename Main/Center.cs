using System.Collections.Generic;

namespace YFramework
{ 
    /// <summary>
    /// ���Ŀ�����
    /// </summary>
    public class Center : ILife
    {
        /// <summary>
        /// ��Ϸģ������
        /// </summary>
        private List<IModule> mGameList;

        public Center()
        {
            mGameList = new List<IModule>();
        }
        #region ��������

        public void Awake()
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                mGameList[i].Awake();
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
        #endregion
        #region ����
        /// <summary>
        /// �����Ϸģ��
        /// </summary>
        /// <param name="game"></param>
        public void AddGame(IModule game)
        {
            if (game == null) return;
            if (mGameList.Contains(game)) return;
            mGameList.Add(game);
        }
        /// <summary>
        /// ��ȡ��Ϸģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetGame<T>() where T : class, IModule
        {
            for (int i = 0; i < mGameList.Count; i++)
            {
                if (mGameList[i] is T) return mGameList[i] as T;
            }
            return default(T);
        }
        /// <summary>
        /// �Ƴ���Ϸģ��
        /// </summary>
        /// <param name="game"></param>
        public void RemoveGame(IModule game)
        {
            if (game == null) return;
            for (int i = 0; i < mGameList.Count; i++)
            {
                if (mGameList[i] == game) {
                    mGameList.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion
    }
}