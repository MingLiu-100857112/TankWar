using System;
using System.Runtime.InteropServices;

namespace AudioLibrary
{
    class API
    {
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            string lpszLongPath,
            string shortFile,
            int cchBuffer);

        /// <summary>
        /// mciSendString：用来播放多媒体文件的API指令
        /// 类型包括：MPEG、AVI、WAV、MP3等等
        /// </summary>
        /// <param name="lpstrCommand">要发送的命令字符串，结构:[命令][设备别名][命令参数]</param>
        /// <param name="lpstrReturnString">返回信息缓冲区：指定大小的字符串变量</param>
        /// <param name="uReturnLength">缓冲区大小：字符串变量的长度</param>
        /// <param name="hwndCallback">回调方式，一般设为零</param>
        /// <returns>函数执行成功返回零，否则返回错误代码</returns>
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback);
    }
}
