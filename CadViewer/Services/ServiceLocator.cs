using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Services
{
	public static class ServiceLocator
	{
		public static IOverlayService OverlayService { get; set; }
	}
}
