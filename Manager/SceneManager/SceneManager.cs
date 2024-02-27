using System;
using System.Collections;
using UnityEngine;

namespace YFramework
{
    public class SceneManager : BaseModule, ISceneManager
    {
        private IMap<string, IScene> mSceneMap;
        public IMap<string, IScene> sceneMap { get { return mSceneMap; } }
        public bool loading { get;private  set; }
        private IScene mCurScene;
        public IScene curScene { get { return mCurScene; } }
        private IScene mPreScene;
        public IScene preScene { get { return mPreScene; } }
        private Action<string> StartLoadScene = null;//开始加载场景回调
        private Action<string> LoadSceneComplete = null;//场景加载完成回调
        public SceneManager(Center center,IMap<string, IScene> map) : base(center)
        {
            if (map == null) return;
            mSceneMap = map;
            foreach (var item in mSceneMap.Keys)
            {
                mSceneMap.Get(item).SetSceneManager(this);
            }
        }
        public override void Update()
        {
            if (mCurScene == null || loading) return;
            mCurScene.Update();
        }
        public override void FixedUpdate()
        {
            if (mCurScene == null || loading) return;
            mCurScene.FixedUpdate();
        }

        public void LoadScene(string sceneName,Action<float> loadAction = null)
        {
            loading = true;
            if ( string.IsNullOrEmpty( sceneName )) 
            {
                Debug.LogError("场景名称不能为空！");
                loading = false;
                return;
            }
            IScene mTempScene = mSceneMap.Get(sceneName);
            if (mTempScene == null)
            {
                Debug.LogError(string .Format("场景名称：{0}  未找到对应的场景类！",sceneName));
                loading = false;
                return;
            }
            mPreScene = mCurScene;
            mCurScene = mTempScene;
            if (mCurScene.sceneName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) {
                //如果当前场景就是现在的场景的话
                mCurScene.Awake();
                mCurScene.Start();
                loading = false;
                return;
            }
            if (mPreScene != null)
            {
                //如果下一个加载的场景跟当前场景相同的话
                if (mPreScene.sceneName == mCurScene.sceneName)
                {
                    loading = false;
                    return;
                }
                mPreScene.OnDestory();
            }
            IEnumeratorModule .StartCoroutine (IELoadScene(loadAction, mTempScene));
        }

        public void ChangeScene(string sceneName) 
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("场景名称不能为空！");
                return;
            }
            IScene mTempScene = mSceneMap.Get(sceneName);
            if (mTempScene == null)
            {
                Debug.LogError(string.Format("场景名称：{0}  未找到对应的场景类！", sceneName));
                return;
            }
            mPreScene = mCurScene;
            mCurScene = mTempScene;
            if (mCurScene.sceneName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
            {
                //如果当前场景就是现在的场景的话
                mCurScene.Awake();
                mCurScene.Start();
                return;
            }
            if (mPreScene != null)
            {
                //如果下一个加载的场景跟当前场景相同的话
                if (mPreScene.sceneName == mCurScene.sceneName)
                {
                    return;
                }
                mPreScene.OnDestory();
            }
            mCurScene.Awake();
            mCurScene.Start();
        }

        private IEnumerator IELoadScene(Action<float> loadAction, IScene tempScene) {
            if (StartLoadScene != null) StartLoadScene.Invoke(tempScene.sceneName);
            AsyncOperation ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(tempScene.sceneName);
            while (!ao.isDone)
            {
                if (loadAction != null) loadAction(ao.progress);
                yield return new WaitForEndOfFrame();
            }
            if(LoadSceneComplete!=null)  LoadSceneComplete(tempScene.sceneName);
            mCurScene.Awake();
            mCurScene.Start();
            loading = false;
        }
        /// <summary>
        /// 设置开始加载的回调
        /// </summary>
        /// <param name="startLoad"></param>
        public void SetStartLoadCallBack(Action<string> startLoad) {
            StartLoadScene = startLoad;
        }
        /// <summary>
        /// 设置开始加载的回调
        /// </summary>
        /// <param name="startLoad"></param>
        public void SetLoadCompleteCallBack(Action<string> loadSceneComplete)
        {
            LoadSceneComplete = loadSceneComplete;
        }
    }
}
