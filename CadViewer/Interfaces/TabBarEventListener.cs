using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Interfaces
{
	public class TabBarItemInfo
	{
		public string strTabName { get; set; }
		public int nTabIdx { get; set; }
		public bool bVisible { get; set; } = true;
		public bool bEnable { get; set; } = true;
	}

	public interface TabBarEventListener
	{
		void TabBar_OnTabChanged(TabBarItemInfo _tabinfo);
	}
}
