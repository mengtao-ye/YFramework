using UnityEngine;

namespace YFramework
{
    public enum AudioType 
    { 
        BG,
        Operator,
        Game,
        Chat,
        Tips,
        MAX_SIZE
    }
    public static class AudioPlayerModule
    {
        private static GameObject mTarget;
        private static AudioSource[] mAS;

        /// <summary>
        /// 设置唤醒播放状态
        /// </summary>
        /// <param name="value"></param>
        public static void SetPlayOnAwake(AudioType type, bool playOnAwake)
        {
            ChechInit();
            mAS[(int)type].playOnAwake = playOnAwake;
        }
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="value"></param>
        public static void SetVolum(AudioType type, float value)
        {
            ChechInit();
            mAS[(int)type].volume = Mathf.Clamp01(value);
        }

      
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="enable"></param>
        public static void SetEnable(AudioType type,bool enable)
        {
            ChechInit();
            mAS[(int)type].enabled = enable;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="isLoop"></param>
        public static void Stop( AudioType type)
        {
            ChechInit();
            mAS[(int)type].Stop();
        }
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="assetPath"></param>
        public static void Play(AudioType type,string assetPath,bool isLoop = false)
        {
            ChechInit();
            mAS[(int)type].clip = Resource.LoadAsset<AudioClip>(assetPath);
            mAS[(int)type].Play();
            mAS[(int)type].loop = isLoop;
        }
        /// <summary>
        /// 检查是否初始化
        /// </summary>
        private static void ChechInit() 
        {
            if (mTarget == null)
            {
                GameObject tempTarget = new GameObject("AudioPlayer");
                GameObject.DontDestroyOnLoad(tempTarget);
                mTarget = tempTarget;
                mAS = new AudioSource[(int)AudioType.MAX_SIZE];
                for (int i = 0; i < (int)AudioType.MAX_SIZE; i++)
                {
                    mAS[i] = tempTarget.AddComponent<AudioSource>();
                    mAS[i].playOnAwake = i == (int)AudioType.BG;
                }
            }
        }
    }
}
