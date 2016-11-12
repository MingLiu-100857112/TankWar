using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AudioLibrary
{
    public class AudioByAPI
    {
        /// <summary>
        /// 打开并播放音频文件
        /// </summary>
        /// <param name="strFileName">音频文件全名</param>
        /// <param name="flagRepeat">是否重复播放</param>
        public static void OpenAndPlay(string strFileName, bool flagRepeat)
        {
            string strCommand = "";
            string buf = "";
            strCommand = "open \"" + strFileName + "\" alias music";
            buf = buf.PadLeft(128, ' ');
            API.mciSendString(strCommand, buf, buf.Length, 0);//打开音频文件
            if (flagRepeat)
            {
                API.mciSendString("play music repeat", null, 0, 0);
            }
            else
            {
                API.mciSendString("play music", null, 0, 0);
            }
            
        }
        /// <summary>
        /// 播放音频文件
        /// </summary>
        /// <param name="flagRepeat">是否重复播放</param>
        public static void Play(bool flagRepeat)
        {
            string strCommand = "";
            if (flagRepeat)
            {
                strCommand = "play music repeat";
            }
            else
            {
                strCommand = "play music";
            }
            API.mciSendString(strCommand, "", 0, 0);//播放音频文件
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public static void Stop()
        {
            API.mciSendString("stop music", "", 0, 0);
        }
        /// <summary>
        /// 关闭音频文件
        /// </summary>
        public static void Close()
        {
            API.mciSendString("close music", "", 0, 0);
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        public static void Pause()
        {
            API.mciSendString("pause music", "", 0, 0);
        }
        /// <summary>
        /// 继续播放
        /// </summary>
        public static void Resume()
        {
            API.mciSendString("resume music", "", 0, 0);
        }
        /// <summary>
        /// 前进到下一个位置
        /// </summary>
        public static void Forward()
        {
            API.mciSendString("step music", "", 0, 0);
        }
        /// <summary>
        /// 前进到下一个位置
        /// </summary>
        public static void Backward()
        {
            API.mciSendString("step music reverse", "", 0, 0);
        }

        /// <summary>
        /// 前进或后退N个步阶
        /// </summary>
        /// <param name="n">调整频数：正数向前，负数后退</param>
        public static void StepN(int nStep)
        {
            API.mciSendString("step music by " + nStep.ToString(), "", 0, 0);
        }
        /// <summary>
        /// 获取当前播放位置
        /// </summary>
        /// <returns>当前播放位置：单位－秒</returns>
        public static int GetCurrentPosition()
        {
            string buf = "";
            buf = buf.PadLeft(128, ' ');
            API.mciSendString("status music position", buf, buf.Length, 0);
            buf = buf.Trim().Replace("\0", "");
            if (string.IsNullOrEmpty(buf))
            {
                return 0;
            }
            else
            {
                return (int)(Convert.ToDouble(buf)) / 1000;
            }
        }
        //总时间
        /// <summary>
        /// 获取播放文件的总长度
        /// </summary>
        /// <returns>播放文件长度：单位－秒</returns>
        public static int GetLenth()
        {
            string strLen = "";
            strLen = strLen.PadLeft(128, ' ');
            API.mciSendString("status music length", strLen, strLen.Length, 0);
            strLen = strLen.Trim().Replace("\0", "");
            if (string.IsNullOrEmpty(strLen))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(strLen) / 1000;
            }
        }
        /// <summary>
        /// 获取当前播放状态信息
        /// </summary>
        /// <returns>播放状态，如播放完毕</returns>
        public static string GetStatus()
        {
            string strStatus = "";
            strStatus = strStatus.PadLeft(128, ' ');
            API.mciSendString("status music mode", strStatus, strStatus.Length, 0);
            return strStatus.Replace("\0", "");
        }
        /// <summary>
        /// 设置音量大小
        /// </summary>
        /// <param name="Valume">音量大小</param>
        /// <returns>设置成功，返回True，否则返回False</returns>
        public static bool SetVolume(int Valume)
        {
            bool success = false;
            string strCommand = string.Format("setaudio music volume to {0}", Valume);
            if (API.mciSendString(strCommand, "", 0, 0) == 0)
            {
                success = true;
            }
            return success;
        }
        /// <summary>
        /// 获取当前音量大小
        /// </summary>
        /// <returns>当前音量大小</returns>
        public static int GetVolume()
        {
            string strVolume = "";
            strVolume = strVolume.PadLeft(128, ' ');
            API.mciSendString("status music volume", strVolume, strVolume.Length, 0);
            int nVolume = 0;
            strVolume = strVolume.Trim().Replace("\0", "");
            if (string.IsNullOrEmpty(strVolume))
            {
                nVolume = 0;
            }
            else
            {
                nVolume = Convert.ToInt32(strVolume);
            }
            return nVolume;
        }

        /// <summary>
        /// 设置指定播放位置
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool SetProcess(int nPosition)
        {
            bool success = false;
            string strCommand = string.Format("seek music to {0}", nPosition);
            if (API.mciSendString(strCommand, "", 0, 0) == 0)
            {
                success = true;
            }
            return success;
        }

    }
}
