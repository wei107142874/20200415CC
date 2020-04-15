using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _50音图
{
	/// <summary>
	/// 扩展方法
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Form设置窗体居中显示
		/// </summary>
		public static void CenterScreen(this Form frm)
		{
			//激活窗体
			frm.Activate();
			//如果窗体是最小化状态，则设置为正常状态
			if (frm.WindowState == FormWindowState.Minimized) frm.WindowState = FormWindowState.Normal;
			//设置窗体位置居中
			frm.Location = new Point((SystemInformation.PrimaryMonitorSize.Width - frm.Width) / 2,
				(SystemInformation.PrimaryMonitorSize.Height - frm.Height) / 2);
		}
	}

}
