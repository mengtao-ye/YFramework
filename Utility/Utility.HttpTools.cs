using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// Http协议数据
        /// </summary>
        public static class HttpTools
        {
            
            /// <summary>
            /// 缓存已经加载的Sprite资源
            /// </summary>
            private static Dictionary<string, Sprite> mTempLoadImgDict = new Dictionary<string, Sprite>();//存放临时图片对象
            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="finish"></param>
            /// <param name="error"></param>
            public static void LoadSprite(string url, Action<Sprite> finish, Action<string> error)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    if (finish != null) finish.Invoke(mTempLoadImgDict[url]);
                    return;
                }
                IEnumeratorModule.StartCoroutine(IELoadSprite(url, finish, error));
            }
            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="finish"></param>
            /// <param name="error"></param>
            public static void LoadSprite<T>(string url, Action<Sprite,T> finish, Action<string> error,T value)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    if (finish != null) finish.Invoke(mTempLoadImgDict[url], value);
                    return;
                }
                IEnumeratorModule.StartCoroutine(IELoadSprite(url, finish, error,value));
            }
            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="finish"></param>
            /// <param name="error"></param>
            public static void LoadSprite(string url, Action<Sprite> finish)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    if (finish != null) finish.Invoke(mTempLoadImgDict[url]);
                    return;
                }
                IEnumeratorModule.StartCoroutine(IELoadSprite(url, finish, LoadSpriteError));
            }
            private static void LoadSpriteError(string error) {
                LogHelper.LogError("Sprite下载异常:" + error);
            }
            private static IEnumerator IELoadSprite<T>(string url, Action<Sprite,T> finish, Action<string> error,T value)
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.result != UnityWebRequest.Result.Success)
                    {
                        error?.Invoke(uwr.error);
                    }
                    else
                    {
                        // 获取下载的纹理 (Texture)
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                        // 将纹理转换为Sprite
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                        // 使用Sprite，例如将其应用到一个GameObject上
                        if (!mTempLoadImgDict.ContainsKey(url))
                        {
                            mTempLoadImgDict.Add(url, sprite);
                        }
                        if (finish != null) finish.Invoke(sprite,value);
                    }
                }
            }
            private static IEnumerator IELoadSprite(string url,  Action<Sprite> finish, Action<string> error)
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.result != UnityWebRequest.Result.Success)
                    {
                        error?.Invoke(uwr.error);
                    }
                    else
                    {
                        // 获取下载的纹理 (Texture)
                        Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                        // 将纹理转换为Sprite
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                        // 使用Sprite，例如将其应用到一个GameObject上
                        if (!mTempLoadImgDict.ContainsKey(url))
                        {
                            mTempLoadImgDict.Add(url, sprite);
                        }
                        if (finish != null) finish.Invoke(sprite);
                    }
                }
            }
        }
    }
}
