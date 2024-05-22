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
            #region 公开函数
            public static void Get(string url, Action<UnityWebRequest> actionResult, Action<string> error)
            {
                IEnumeratorModule.StartCoroutine(GetAsyn(url, actionResult, error));
            }
            public static void GetTexture(string url, Action<Texture2D> actionResult, Action<string> error)
            {
                IEnumeratorModule.StartCoroutine(GetTextureAsyn(url, actionResult, error));
            }

            public static void GetBytesToLocal(string url, string localPath, Action<string> error,Action<int > size)
            {
                IEnumeratorModule.StartCoroutine(GetBytesToLocalAsyn(url, localPath, error,size));
            }

            public static void GetBytes(string url, Action<float> process,Action<byte[]> actionResult, Action<string> error)
            {
                IEnumeratorModule.StartCoroutine(GetBytesAsyn(url, process, actionResult, error));
            }
            public static void GetText(string url, Action<string> actionResult, Action<string> error)
            {
                IEnumeratorModule.StartCoroutine(GetTextAsyn(url, actionResult, error));
            }
            public static void GetAssetBundle(string url, Action<AssetBundle> actionResult,Action<string> error)
            {
               IEnumeratorModule.  StartCoroutine(GetAssetBundleAsyn(url, actionResult, error));
            }
            #endregion
            #region 私有函数

            private static IEnumerator GetAsyn(string url, Action<UnityWebRequest> actionResult,Action<string> error)
            {
                using (UnityWebRequest uwr = UnityWebRequest.Get(url))
                {
                    yield return uwr.SendWebRequest();
                    if (uwr.result == UnityWebRequest.Result.Success) {
                        actionResult?.Invoke(uwr);
                    }
                    else
                    {
                        error?.Invoke("GetAsyn Error:" + uwr.error);
                    }
                }
            }

            private static IEnumerator GetTextureAsyn(string url, Action<Texture2D> actionResult,Action<string> error)
            {
                UnityWebRequest uwr = new UnityWebRequest(url);
                DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
                uwr.downloadHandler = downloadTexture;
                yield return uwr.SendWebRequest();
                if (uwr.result == UnityWebRequest.Result.Success) 
                {
                    Texture2D t = downloadTexture.texture;
                    if (t == null) Debug.LogError("GetTextureAsyn()/ Get Texture is error! url:" + url);
                    actionResult?.Invoke(t);
                }
                else
                {
                    error?.Invoke("Load Texture Error:" + uwr.error);
                }
            }

            private static IEnumerator GetBytesToLocalAsyn(string url,string localPath, Action<string> error, Action<int> size)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    size?.Invoke(request.downloadHandler.data.Length);
                    FileTools.Write( localPath, request.downloadHandler.data);
                }
                else
                {
                    error?.Invoke("Load bytes Error:" + request.error);
                }
            }

            private static IEnumerator GetBytesAsyn(string url,Action<float> process, Action<byte[]> actionResult, Action<string> error)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                request.SendWebRequest();
                while (!request.isDone)
                {
                    process?.Invoke(request.downloadProgress);
                    yield return 0;
                }
                if (request.result == UnityWebRequest.Result.Success)
                {
                    actionResult?.Invoke(request.downloadHandler.data);
                }
                else
                {
                    error?.Invoke("Load bytes Error:" + request.error);
                }
            }
            private static IEnumerator GetTextAsyn(string url, Action<string> actionResult,Action<string> error)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    string t = request.downloadHandler.text;
                    actionResult?.Invoke(t);
                }
                else
                {
                    error?.Invoke("Load Txt Error:" + request.error);
                }
            }
            /// <summary>
            /// 异步加载AB包
            /// </summary>
            /// <param name="url"></param>
            /// <param name="actionResult"></param>
            /// <returns></returns>
            private static IEnumerator GetAssetBundleAsyn(string url, Action<AssetBundle> actionResult,Action<string> error)
            {
                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle)?.assetBundle;
                    if (ab == null)
                    {
                        error?.Invoke("Failed to load AssetBundle!");
                        yield break;
                    }
                    actionResult?.Invoke(ab);
                }
                else 
                {
                    error?.Invoke("Load ABAsset Error:"+request.error);
                }
            }
        #endregion
            #region LoadSprite
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
            public static void LoadSprite<T>(string url, Action<Sprite, T> finish, Action<string> error, T value)
            {
                if (string.IsNullOrEmpty(url)) return;
                if (mTempLoadImgDict.ContainsKey(url))
                {
                    if (finish != null) finish.Invoke(mTempLoadImgDict[url], value);
                    return;
                }
                IEnumeratorModule.StartCoroutine(IELoadSprite(url, finish, error, value));
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
            private static void LoadSpriteError(string error)
            {
                LogHelper.LogError("Sprite下载异常:" + error);
            }
            private static IEnumerator IELoadSprite<T>(string url, Action<Sprite, T> finish, Action<string> error, T value)
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
                        if (finish != null) finish.Invoke(sprite, value);
                    }
                }
            }
            private static IEnumerator IELoadSprite(string url, Action<Sprite> finish, Action<string> error)
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
            #endregion
        }
    }
}
