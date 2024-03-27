using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            /// <param name="img"></param>
            /// <param name="finish"></param>
            public static void LoadSprite<T>(string url, Action<Sprite,T> finish ,Action<string> error,T value)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    if (finish != null) finish.Invoke(mTempLoadImgDict[url],value);
                    return;
                }
                IEnumeratorModule.StartCoroutine(GetSprite(url, finish, error, value));
            }

            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="img"></param>
            /// <param name="finish"></param>
            public static void LoadSprite(string url, Action<Sprite> finish = null)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {

                    if (finish != null) finish.Invoke(mTempLoadImgDict[url]);
                    return;
                }
                IEnumeratorModule.StartCoroutine(GetSprite(url, finish));
            }
            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="url"></param>
            /// <param name="img"></param>
            /// <param name="finish"></param>
            public static void LoadImage(string url, Image img, Action finish = null)
            {
                if (string.IsNullOrEmpty(url) || img == null) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    img.sprite = mTempLoadImgDict[url];
                    if (finish != null) finish.Invoke();
                    return;
                }
                IEnumeratorModule.StartCoroutine(GetImage(url, img, finish));
            }
            private static IEnumerator GetSprite<T>(string url, Action<Sprite,T> finish,Action<string> fail,T value)
            {
                WWW www = new WWW(url);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D tex = www.texture;
                    Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                    if (!mTempLoadImgDict.ContainsKey(url))
                    {
                        mTempLoadImgDict.Add(url, temp);
                    }
                    if (finish != null) finish.Invoke(temp,value);
                }
                else
                {
                    if (fail != null) fail.Invoke(www.error);
                }
            }
            private static IEnumerator GetSprite(string url, Action<Sprite> finish)
            {
                WWW www = new WWW(url);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D tex = www.texture;
                    Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                    if (!mTempLoadImgDict.ContainsKey(url))
                    {
                        mTempLoadImgDict.Add(url, temp);
                    }
                    if (finish != null) finish.Invoke(temp);
                }
                else
                {
                    if (finish != null) finish.Invoke(null);
                }
            }
            private static IEnumerator GetImage(string url, Image img, Action finish)
            {
                WWW www = new WWW(url);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D tex = www.texture;
                    Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                    img.sprite = temp; //设置的图片，显示从URL图片
                    if (!mTempLoadImgDict.ContainsKey(url))
                    {
                        mTempLoadImgDict.Add(url, temp);
                    }
                    if (finish != null) finish.Invoke();
                }
                else
                {
                    if (finish != null) finish.Invoke();
                }
            }
        }
    }
}
