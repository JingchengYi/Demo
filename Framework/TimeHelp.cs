using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Framework
{
    public class TimeHelp
    {
        #region 日期和unix时间戳转换
        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(dateTime - startTime).TotalSeconds; // 相差秒数
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(DateTime target, long timestamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddSeconds(timestamp);
        }
        #endregion       
     
        #region 设置本机时间
        /// <summary>
        /// 设置本地电脑的年月日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void SetLocalDate(int year, int month, int day)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c date {0}-{1}-{2}", year, month, day);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的时分秒
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        public static void SetLocalTime(int hour, int min, int sec)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c time {0}:{1}:{2}", hour, min, sec);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的年月日和时分秒
        /// </summary>
        /// <param name="time"></param>
        public static void SetLocalDateTime(DateTime time)
        {
            SetLocalDate(time.Year, time.Month, time.Day);
            SetLocalTime(time.Hour, time.Minute, time.Second);
        } 
        #endregion
    }
}
