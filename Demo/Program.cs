using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo
{
    static class Program
    {
        static KeyboardHook keyboardHook = new KeyboardHook();//new钩子对象
        static NotifyIcon icon = null;//图标控件对象
        static System.Threading.Timer tmr = null;//计时器对象

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region 添加和启用钩子
            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            if (AppHelp.GetAppSetting("IsKey") == "")
            {
                AppHelp.SaveAppSetting("IsKey", "T");
            }
            if (AppHelp.GetAppSetting("IsKey") == "T")
            {
                keyboardHook.Start();
            }
            #endregion

            #region 根据借口API设置本地时间
            new Thread(() =>
               {
                   var result = WebHelp.HttpGet("https://www.immomo.com/login", "action=captcha");
                   JObject obj = JObject.Parse(result);
                   var time = TimeHelp.UnixTimestampToDateTime(new DateTime(), long.Parse(obj["timesec"].ToString()));
                   TimeHelp.SetLocalDateTime(time);
               }).Start();
            #endregion

            #region 默认设为开机启动
            new Thread(() =>
                {
                    if (AppHelp.GetAppSetting("IsStart") == "")
                    {
                        PCStartHelp.AddStart(Application.ExecutablePath + "Demo.exe", "y");
                        AppHelp.SaveAppSetting("IsStart", "T");
                    }
                }).Start();
            #endregion

            #region 判断时间有没有超过并设置定时器
            tmr = new System.Threading.Timer((o) =>
                {
                    MessageBox.Show("打下班卡!!!!!!!!!!!!!!!!");
                }, "", DateTime.Now < DateTime.Now.Date.AddHours(18) ? (long)((DateTime.Now.Date.AddHours(18) - DateTime.Now).TotalMilliseconds) : 0, Timeout.Infinite);
            #endregion

            #region 初始化图标控件对象
            icon = new NotifyIcon
                {
                    Icon = Properties.Resources.y,
                    Visible = true,
                    ContextMenu = new ContextMenu(new MenuItem[]{
                    new MenuItem("注释代码",Program_Click){Checked=AppHelp.GetAppSetting("IsKey")=="T"},
                    new MenuItem("开机启动",Program_Click){Checked=AppHelp.GetAppSetting("IsStart")=="T"},
                    new MenuItem("退出程序",Program_Click)
                })
                };
            #endregion

            Application.Run();
        }

        /// <summary>
        /// 钩子触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        SendKeys.Send("*|*[" + DateTime.Now.ToShortDateString() + "]YJC   ");
                        break;
                    case Keys.D2:
                        SendKeys.Send("&&[" + DateTime.Now.ToShortDateString() + "]YJC   ");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Program_Click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            switch (item.Text)
            {
                case "注释代码":
                    item.Checked = !item.Checked;
                    if (item.Checked)
                    {
                        keyboardHook.Start();
                        AppHelp.SaveAppSetting("IsKey", "T");
                    }
                    else
                    {
                        keyboardHook.Stop();
                        AppHelp.SaveAppSetting("IsKey", "F");
                    }
                    break;
                case "开机启动":
                    item.Checked = !item.Checked;
                    if (item.Checked)
                    {
                        PCStartHelp.AddStart(Application.ExecutablePath + "Demo.exe", "y");
                        AppHelp.SaveAppSetting("IsStart", "T");
                    }
                    else
                    {
                        PCStartHelp.RemoveStart("y");
                        AppHelp.SaveAppSetting("IsStart", "F");
                    }
                    break;
                case "退出程序":
                    icon.Dispose();
                    tmr.Dispose();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
