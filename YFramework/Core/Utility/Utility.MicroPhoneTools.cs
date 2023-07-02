using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 麦克分工具
        /// </summary>
        public class MicrophoneTools : Singleton<MicrophoneTools>
        {
            protected AudioSource audioSource;//音频资源对象
            public int lengthSec = 6;//录制的时长
            public int frequency = 11000;//录制的频率
            public MicrophoneTools()
            {
                audioSource = new GameObject("AudioTools").AddComponent<AudioSource>();
            }
            /// <summary>
            /// 请求权限
            /// </summary>
            /// <returns></returns>
            public IEnumerator RequestMicrophoneAuth()
            {
                yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
            }
            /// <summary>
            /// 开始录制
            /// </summary>
            public void StartRecord()
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.mute = true;
                audioSource.clip = Microphone.Start(null, false, 10, frequency);
                audioSource.Play();
            }
            /// <summary>
            /// 结束录制
            /// </summary>
            /// <returns></returns>
            public int StopRecord()
            {
                int length = Microphone.GetPosition(null);
                Microphone.End(null);
                audioSource.Stop();
                return length;
            }
            /// <summary>
            /// 保存录制文件信息
            /// </summary>
            /// <param name="length"></param>
            /// <returns></returns>
            public byte[] SaveAudioFile(int length)
            {
                if (Microphone.IsRecording(null))
                    return null;
                return GetClipData(audioSource, length);
            }
            /// <summary>
            /// 读取录制文件信息
            /// </summary>
            /// <param name="bytes"></param>
            public void ReadAudioFile(byte[] bytes)
            {
                if (bytes == null || bytes.Length == 0) return;
                float[] samples = byte2float(bytes);
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                audioSource.clip = AudioClip.Create("audioChat", samples.Length, 1, frequency, false);
                audioSource.clip.SetData(samples, 0);
                audioSource.mute = false;
                audioSource.Play();
            }
            /// <summary>
            /// 获取录制的语音信息
            /// </summary>
            /// <param name="source"></param>
            /// <param name="length"></param>
            /// <returns></returns>
            byte[] GetClipData(AudioSource source, int length)
            {
                if (length < 10)
                {
                    Debug.Log("录音文件太短");
                    return null;
                }

                float[] samples = new float[length];
                source.clip.GetData(samples, 0);

                byte[] outData = float2byte(samples);
                return outData;
            }
            /// <summary>
            /// 将float的值转换成byte数组
            /// </summary>
            /// <param name="floats"></param>
            /// <returns></returns>
            byte[] float2byte(float[] floats)
            {
                byte[] outData = new byte[floats.Length * 2];
                int reScaleFactor = 32767;

                for (int i = 0; i < floats.Length; i++)
                {
                    short tempShort = (short)(floats[i] * reScaleFactor);
                    byte[] tempData = BitConverter.GetBytes(tempShort);

                    outData[i * 2] = tempData[0];
                    outData[i * 2 + 1] = tempData[1];
                }

                return outData;
            }
            /// <summary>
            /// 将byte数组转换成float数据
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            float[] byte2float(byte[] bytes)
            {
                float reScaleFactor = 32768.0f;
                float[] data = new float[bytes.Length / 2];
                for (int i = 0; i < bytes.Length; i += 2)
                {
                    short s;
                    if (BitConverter.IsLittleEndian) //小端和大端顺序要调整
                        s = (short)((bytes[i + 1] << 8) | bytes[i]);
                    else
                        s = (short)((bytes[i] << 8) | bytes[i + 1]);
                    // convert to range from -1 to (just below) 1
                    data[i / 2] = s / reScaleFactor;
                }

                return data;
            }
        }
    }

}